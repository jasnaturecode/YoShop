using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 小程序管理
    /// </summary>
    public class WxAppController : Controller
    {
        /// <summary>
        /// 小程序设置
        /// </summary>
        /// <returns></returns>
        public IActionResult Setting()
        {
            return View();
        }

        /// <summary>
        /// 页面设计
        /// </summary>
        /// <returns></returns>
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// 页面链接
        /// </summary>
        /// <returns></returns>
        public IActionResult Links()
        {
            return View();
        }

        /// <summary>
        /// 帮助中心
        /// </summary>
        /// <returns></returns>
        public IActionResult Help()
        {
            return View();
        }

        /// <summary>
        /// 导航设置
        /// </summary>
        /// <returns></returns>
        public IActionResult TabBar()
        {
            return View();
        }
    }
}