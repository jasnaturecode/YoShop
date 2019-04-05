﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class GoodsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/goods.category/index")]
        public IActionResult CategoryIndex()
        {
            return View();
        }
    }
}