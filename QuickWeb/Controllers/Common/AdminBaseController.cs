using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Core.Net;
using Microsoft.AspNetCore.Mvc;
using Quick.Common;
using Quick.Models.Dto;

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
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool IsAdminLogin()
        {
            return GetAdminSession() != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected AdminDto GetAdminSession()
        {
            if (IsDebug)
            {
                var dto = new AdminDto { store_user_id = 1, user_name = "admin", wxapp_id = 10001, create_time = 1529926348, update_time = 1531027042 };
                HttpContext.Session.Set(SessionKey.AdminInfo, dto);
                return dto;
            }
            else
                return HttpContext.Session.Get<AdminDto>(SessionKey.AdminInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        protected void SetAdminSession(AdminDto dto)
        {
            HttpContext.Session.Set(SessionKey.AdminInfo, dto);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetAdminLogOut()
        {
            HttpContext.Session.Remove(SessionKey.AdminInfo);
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("password");
            HttpContext.Session.Clear();
        }

        #endregion


    }
}