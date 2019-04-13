using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Core.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Quick.Common;
using Quick.IService;
using Quick.Models.Dto;
using QuickWeb.Extensions;
using QuickWeb.Extensions.Common;

namespace QuickWeb.Controllers.Common
{
    /// <summary>
    /// 管理员公共控制器
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminBaseController : BaseController
    {
        #region 管理员用户Session相关操作

        /// <summary>
        /// 管理员是否登录
        /// </summary>
        /// <returns></returns>
        protected bool IsAdminLogin()
        {
            return GetAdminSession() != null;
        }

        /// <summary>
        /// 获取管理员登录信息
        /// </summary>
        /// <returns></returns>
        protected AdminDto GetAdminSession()
        {
            return HttpContext.Session.Get<AdminDto>(SessionKey.AdminInfo);
        }

        /// <summary>
        /// 设置管理员登录信息
        /// </summary>
        /// <param name="dto"></param>
        protected void SetAdminSession(AdminDto dto)
        {
            HttpContext.Session.Set(SessionKey.AdminInfo, dto);
        }

        /// <summary>
        /// 注销管理员账户
        /// </summary>
        protected void SetAdminLogOut()
        {
            HttpContext.Session.Remove(SessionKey.AdminInfo);
            Response.Cookies.Delete("admin_username");
            Response.Cookies.Delete("admin_password");
            HttpContext.Session.Clear();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (AppConfig.IsDebug)
            {
                if (!IsAdminLogin())
                {
                    var adminDto = new AdminDto(){ store_user_id = 10001,wxapp_id = 10001, user_name = "admin" };
                    context.HttpContext.Session.Set(SessionKey.AdminInfo, adminDto);
                }
            }
            base.OnActionExecuting(context);
        }

    }
}