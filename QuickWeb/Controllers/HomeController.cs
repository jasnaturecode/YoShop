using Microsoft.AspNetCore.Mvc;
using QuickWeb.Models;
using System.Diagnostics;
using QuickWeb.Controllers.Common;
using System;
using Masuit.Tools.Logging;
using QuickWeb.Models.RequestModel;
using Quick.IService;
using Quick.Common;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 管理首页
    /// </summary>
    public class HomeController : AdminBaseController
    {
        /// <summary>
        /// yoshop_store_user对象业务方法
        /// </summary>
        public Iyoshop_store_userService StoreUserService { get; set; }

        #region 管理首页
        /// <summary>
        /// 管理首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/store.user/renew")]
        public IActionResult RenewPassword()
        {
            return View(new AdminPwdRequest() { user_name = GetAdminSession().user_name });
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("/store.user/renew"), AutoValidateAntiforgeryToken]
        public IActionResult RenewPassword(AdminPwdRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.password) || string.IsNullOrEmpty(request.password_new) || string.IsNullOrEmpty(request.password_confirm))
                    return No("密码不能为空");
                if (request.password_confirm != request.password_new)
                    return No("密码不一致");
                var loginUser = StoreUserService.Login(GetAdminSession().user_name, request.password);
                if (loginUser == null)
                    return No("原密码不正确");
                StoreUserService.ChangePwd(loginUser.store_user_id, request.password_new);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Yes("更新成功！");
        }
        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/passport/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.AdminInfo);
            Response.Cookies.Delete("admin_username");
            Response.Cookies.Delete("admin_password");
            HttpContext.Session.Clear();
            if (Request.Method.ToLower().Equals("get"))
                return Redirect(QuickKeys.AdminLogin);
            return Yes("注销成功！");
        }
        #endregion

        #region 错误页面
        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
