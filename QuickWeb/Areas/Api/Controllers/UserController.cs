using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quick.IService;

namespace QuickWeb.Areas.Api.Controllers
{
    /// <summary>
    /// 用户接口
    /// </summary>
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// yoshop_user对象业务逻辑方法
        /// </summary>
        public Iyoshop_userService UserService { get; set; }

        /// <summary>
        /// 根据用户Id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Route("/user/{id}")]
        public OkObjectResult Get(int id)
        {
            var data = UserService.GetById(id);
            return Ok(data);
        }
    }
}