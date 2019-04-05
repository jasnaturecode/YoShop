using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}