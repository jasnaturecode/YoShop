using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SettingController : AdminBaseController
    {

        /// <summary>
        /// 商城设置
        /// </summary>
        /// <returns></returns>
        public IActionResult Store()
        {
            return View();
        }

        /// <summary>
        /// 交易设置
        /// </summary>
        /// <returns></returns>
        public IActionResult Trade()
        {
            return View();
        }

        /// <summary>
        /// 配送设置
        /// </summary>
        /// <returns></returns>
        public IActionResult Delivery()
        {
            return View();
        }

        /// <summary>
        /// 短信设置
        /// </summary>
        /// <returns></returns>
        public IActionResult Sms()
        {
            return View();
        }

        /// <summary>
        /// 上传设置
        /// </summary>
        /// <returns></returns>
        public IActionResult Storage()
        {
            return View();
        }

        #region 其他设置

        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        public IActionResult Clear()
        {
            return View();
        }

        /// <summary>
        /// 环境检测
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}