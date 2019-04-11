using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Masuit.Tools;
using Masuit.Tools.AspNetCore.ResumeFileResults.Extensions;
using Masuit.Tools.Core.Net;
using Masuit.Tools.Security;
using Masuit.Tools.Strings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quick.Common;
using Quick.IService;
using Quick.Models.Dto;
using QuickWeb.Controllers.Common;
using QuickWeb.Extensions;
using QuickWeb.Models.RequestModel;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 管理员登录授权
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LoginController : BaseController
    {
        /// <summary>
        /// yoshop_store_user对象业务方法
        /// </summary>
        public Iyoshop_store_userService StoreUserService { get; set; }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/passport/login")]
        public IActionResult Index()
        {
            string from = Request.Query["from"];
            if (!string.IsNullOrEmpty(from))
            {
                from = HttpUtility.UrlDecode(from);
                Response.Cookies.Append("refer", from);
            }
            if (HttpContext.Session.Get<AdminDto>(SessionKey.AdminInfo) != null)
            {
                if (string.IsNullOrEmpty(from))
                    from = "/home/index";
                return Redirect(from);
            }
            if (Request.Cookies.Count > 2)
            {
                string name = Request.Cookies["admin_username"];
                string pwd = Request.Cookies["admin_password"]?.DesDecrypt(AppConfig.BaiduAK);
                var userInfo = StoreUserService.Login(name, pwd);
                if (userInfo != null)
                {
                    Response.Cookies.Append("admin_username", name, new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                    Response.Cookies.Append("admin_password", Request.Cookies["admin_password"], new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                    HttpContext.Session.Set(SessionKey.UserInfo, userInfo);
                    //HangfireHelper.CreateJob(typeof(IHangfireBackJob), nameof(HangfireBackJob.LoginRecord), "default", userInfo, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(), LoginType.Default);
                    if (string.IsNullOrEmpty(from))
                        from = QuickKeys.AdminHome;
                    return Redirect(from);
                }
            }
            return View(new AdminLoginRequest());
        }

        /// <summary>
        /// 登陆检查
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost,Route("/passport/login"), ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginRequest request)
        {
            //string validSession = HttpContext.Session.Get<string>("valid") ?? string.Empty; //将验证码从Session中取出来，用于登录验证比较
            //if (string.IsNullOrEmpty(validSession) || !valid.Trim().Equals(validSession, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return No("验证码错误");
            //}
            //HttpContext.Session.Remove("valid"); //验证成功就销毁验证码Session，非常重要

            if (string.IsNullOrEmpty(request.user_name.Trim()) || string.IsNullOrEmpty(request.password.Trim()))
            {
                return No("用户名或密码不能为空");
            }
            var userInfo = StoreUserService.Login(request.user_name, request.password);
            if (userInfo != null)
            {
                HttpContext.Session.Set(SessionKey.UserInfo, userInfo);
                if (request.remember.Trim().Contains(new[] { "on", "true" })) //是否记住登录
                {
                    Response.Cookies.Append("admin_username", HttpUtility.UrlEncode(request.user_name.Trim()), new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                    Response.Cookies.Append("admin_password", request.password.Trim().DesEncrypt(AppConfig.BaiduAK), new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                }
                //HangfireHelper.CreateJob(typeof(IHangfireBackJob), nameof(HangfireBackJob.LoginRecord), "default", userInfo, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(), LoginType.Default);
                string refer = Request.Cookies["refer"];
                return YesRedirect("登陆成功！", string.IsNullOrEmpty(refer) ? "/" : refer);
            }
            return No("用户名或密码错误");
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            string code = Masuit.Tools.Strings.ValidateCode.CreateValidateCode(6);
            HttpContext.Session.Set("valid", code); //将验证码生成到Session中
            var buffer = HttpContext.CreateValidateGraphic(code);
            return this.ResumeFile(buffer, "image/jpeg");
        }

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckValidateCode(string code)
        {
            string validSession = HttpContext.Session.Get<string>("valid");
            if (string.IsNullOrEmpty(validSession) || !code.Trim().Equals(validSession, StringComparison.InvariantCultureIgnoreCase))
            {
                return No("验证码错误");
            }
            return Yes("验证码正确");
        }

    }
}