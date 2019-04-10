using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickWeb.Controllers.Common;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderController : AdminBaseController
    {
        /// <summary>
        /// 待发货
        /// </summary>
        /// <returns></returns>
        public IActionResult Delivery_List()
        {
            return View();
        }

        /// <summary>
        /// 待收货
        /// </summary>
        /// <returns></returns>
        public IActionResult Receipt_List()
        {
            return View();
        }

        /// <summary>
        /// 待付款
        /// </summary>
        /// <returns></returns>
        public IActionResult Pay_List()
        {
            return View();
        }

        /// <summary>
        /// 已完成
        /// </summary>
        /// <returns></returns>
        public IActionResult Complete_List()
        {
            return View();
        }

        /// <summary>
        /// 已取消
        /// </summary>
        /// <returns></returns>
        public IActionResult Cancel_List()
        {
            return View();
        }

        /// <summary>
        /// 全部订单
        /// </summary>
        /// <returns></returns>
        public IActionResult All_List()
        {
            return View();
        }
    }
}