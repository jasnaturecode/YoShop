using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Masuit.Tools.Core.Linq;
using Microsoft.AspNetCore.Mvc;
using Quick.IService;
using Quick.Models.Entity.Table;
using QuickWeb.Controllers.Common;
using QuickWeb.Extensions.Common;
using QuickWeb.Models.ViewModel;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class GoodsController : AdminBaseController
    {
        /// <summary>
        /// yoshop_category对象业务方法
        /// </summary>
        public Iyoshop_categoryService CategoryService { get; set; }

        /// <summary>
        /// yoshop_goods对象业务方法
        /// </summary>
        public Iyoshop_goodsService GoodsService { get; set; }


        public IActionResult Index()
        {
            return View();
        }

        #region 商品分类
        /// <summary>
        /// 商品分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/goods.category/index")]
        public async Task<IActionResult> CategoryIndex()
        {
            var list = await GetCategories(l => true);
            return View(list);
        }

        [HttpGet, Route("/goods.category/add")]
        public async Task<IActionResult> CategoryAdd()
        {
            var list = await GetCategories(l => l.parent_id == 0);
            ViewData["first"] = list;
            return View(new CategoryViewModel());
        }

        [HttpPost, Route("/goods.category/add")]
        public IActionResult CategoryAdd(CategoryViewModel viewModel)
        {
            return View();
        }

        [HttpGet, Route("/goods.category/edit/category_id/{id}")]
        public IActionResult CategoryEdit(uint id)
        {
            var model = CategoryService.GetById(id);
            if(model == null) return View();
            var list = GetCategories(l => l.parent_id == 0);
            ViewData["first"] = list;
            return View();
        }

        [HttpPost, Route("/goods.category/delete/category_id/{id}")]
        public IActionResult CategoryDelete(int id)
        {
            return View();
        }


        private async Task<List<CategoryViewModel>> GetCategories(Expression<Func<yoshop_category, bool>> where)
        {
            Expression<Func<yoshop_category, bool>> cond = l => l.wxapp_id == GetAdminSession().wxapp_id;
            cond = cond.And(where);
            var list = await CategoryService.LoadOrderedEntities<int>(cond, s => s.sort, true).ToListAsync();
            return list.Mapper<List<CategoryViewModel>>();
        }

        #endregion

    }
}