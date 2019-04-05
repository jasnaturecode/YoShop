﻿using Microsoft.AspNetCore.Mvc;
using QuickWeb.Models;
using System.Diagnostics;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 管理首页
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
