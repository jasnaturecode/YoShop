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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using QuickWeb.Extensions.L2Cache;
using Masuit.Tools.Logging;
using System.IO;
using Quick.Models.Entity.Table;

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
            Configuration.GetSectionValue<AppConfig>();
            //配置数据库
            DbContext.SqlString = Configuration.GetConnectionString("Mysql");
            //读取Redis缓存配置
            Configuration.GetSectionValue<RedisConfig>();
            // 配置Mvc模式和其他配置
            services.AddMvc(options =>
            {
                // 缓存配置文件(默认30秒钟) [ResponseCache(CacheProfileName = "Default30")] 响应缓存在 ASP.NET Core | Microsoft Docs https://docs.microsoft.com/zh-cn/aspnet/core/performance/caching/response?view=aspnetcore-2.2
                //options.CacheProfiles.Add("Default30", new CacheProfile() { Duration = 30 });
            }).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddControllersAsServices();
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
            // 配置MemoryCache缓存
            services.AddMemoryCache();

            if (RedisConfig.UseRedis)
            {
                // //初始化 RedisHelper  nuget Install-Package CSRedisCore  https://github.com/2881099/csredis
                RedisHelper.Initialization(new CSRedisClient(RedisConfig.ConnectionString));

                // 注册mvc分布式缓存（暂时不用）  nuget Install-Package Caching.CSRedis
                //services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));

                //配置Hangfire
                services.AddHangfire(x => x.UseRedisStorage(RedisConfig.ConnectionString));

                if (RedisConfig.UseRedisCache)
                {
                    //Use Redis
                    services.AddSingleton<ICacheService, RedisCacheService>();
                }
            }
            else
            {
                //Use MemoryCache
                services.AddSingleton<IMemoryCache>(factory =>
                {
                    var cache = new MemoryCache(new MemoryCacheOptions());
                    return cache;
                });
                services.AddSingleton<ICacheService, MemoryCacheService>();

                //配置Hangfire
                services.AddHangfire(x => x.UseMemoryStorage());
            }

            //配置7z和断点续传
            services.AddSevenZipCompressor().AddResumeFileResult();
            //注入SignalR
            services.AddWebSockets(opt => opt.ReceiveBufferSize = 4096 * 1024).AddSignalR();
            // 配置Https跳转
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
            //});
            //解决razor视图中中文被编码的问题
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces().Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service") || t.Name.EndsWith("Controller")).PropertiesAutowired().AsSelf().InstancePerDependency(); //注册控制器为属性注入
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
                app.UseExceptionHandler("/Error/Index");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();

                app.UseException();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions //静态资源缓存策略
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,no-cache";
                    ctx.Context.Response.Headers[HeaderNames.Expires] = DateTime.UtcNow.AddDays(7).ToString("R");
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
                c.RoutePrefix = "swagger";
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
