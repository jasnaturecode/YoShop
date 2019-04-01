using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSRedis;
using Hangfire;
using Hangfire.MemoryStorage;
using Masuit.Tools.AspNetCore.Mime;
using Masuit.Tools.Core.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quick.Models.Application;
using QuickWeb.Extensions;
using QuickWeb.Extensions.Common;
using QuickWeb.Extensions.Hangfire;
using QuickWeb.Hubs;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Quick.Service;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace QuickWeb
{
    /// <summary>
    /// asp.net core核心配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// asp.net core核心配置
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 属性对象
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //配置Cookie策略
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //读取应用程序配置
            AppConfig.Init(Configuration);
            //配置数据库和Redis连接字符串
            DbContext.SqlString = Configuration.GetConnectionString("Mysql");
            RedisHelper.Initialization(new CSRedisClient(Configuration.GetConnectionString("Redis")));
            // 配置Mvc模式和其他配置
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //配置跨域
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyOrigin();
                    p.AllowCredentials();
                });
            });
            //配置swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "API文档",
                    Version = "v1"
                });
                c.DescribeAllEnumsAsStrings();
                c.IncludeXmlComments(AppContext.BaseDirectory + "QuickWeb.xml");
            });
            //注入HttpClient
            services.AddHttpClient();
            //注入静态HttpContext
            services.AddHttpContextAccessor();
            //注入响应缓存
            services.AddResponseCaching();
            //配置请求长度
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600;// 100MB
            });
            //注入Session
            services.AddSession();
            //配置Hangfire
            //services.AddHangfire(x => x.UseRedisStorage(Configuration.GetConnectionString("Redis")));
            services.AddHangfire(x => x.UseMemoryStorage());
            //配置7z和断点续传
             services.AddSevenZipCompressor().AddResumeFileResult();
            //注入SignalR
            services.AddWebSockets(opt => opt.ReceiveBufferSize = 4096 * 1024).AddSignalR();
            //
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
            });
            //解决razor视图中中文被编码的问题
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly).AsImplementedInterfaces().Where(t => t.Name.EndsWith("Service")).PropertiesAutowired().AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces().Where(t => t.Name.EndsWith("Controller")).PropertiesAutowired().AsSelf().InstancePerDependency(); //注册控制器为属性注入
            builder.RegisterType<BackgroundJobClient>().SingleInstance(); //指定生命周期为单例
            builder.RegisterType<HangfireBackJob>().As<IHangfireBackJob>().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues).InstancePerDependency();
            AutofacContainer = new AutofacServiceProvider(builder.Build());
            return AutofacContainer;
        }

        /// <summary>
        /// 依赖注入容器
        /// </summary>
        public static IServiceProvider AutofacContainer { get; set; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                app.UseException();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions //静态资源缓存策略
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers[HeaderNames.CacheControl] = "public,no-cache";
                    context.Context.Response.Headers[HeaderNames.Expires] = DateTime.UtcNow.AddDays(7).ToString("R");
                },
                ContentTypeProvider = new FileExtensionContentTypeProvider(MimeMapper.MimeTypes)
            }).UseCookiePolicy();
            // URL重写
            app.UseRewriter(new RewriteOptions().AddRedirectToNonWww());
            //注入静态HttpContext对象
            app.UseStaticHttpContext();
            //注入Session
            app.UseSession();
            //启用网站防火墙
            app.UseRequestIntercept();
            //配置hangfire
            app.UseHangfireServer().UseHangfireDashboard("/taskcenter", new DashboardOptions()
            {
                Authorization = new[]
                {
                    new MyRestrictiveAuthorizationFilter()
                }
            });
            //配置跨域
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
                builder.AllowCredentials();
            });
            //启动Response缓存
            app.UseResponseCaching();
            //配置swagger
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", AppConfig.AppTitle);
            });
            //配置SignalR
            app.UseSignalR(hub => hub.MapHub<MyHub>("/hubs"));
            //初始化定时任务
            HangfireConfig.Start();
            //配置默认路由
            app.UseMvcWithDefaultRoute();
        }
    }
}
