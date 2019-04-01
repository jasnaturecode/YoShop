using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quick.Common.Models;

namespace QuickWeb.Controllers.Common
{
    /// <summary>
    /// 基础公共控制器
    /// </summary>
    public class BaseController : Controller
    {
        #region 通用返回JsonResult的封装

        /// <summary>
        /// 只返回响应状态和提示信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        protected ContentResult Build(int status, string msg)
        {
            var js = new ResultInfo(status, msg);
            return Build(js);
        }

        /// <summary>
        /// 返回响应状态、消息和数据对象
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected ContentResult Build(int status, string msg, object data)
        {
            var js = new ResultInfo(status, msg, data);
            return Build(js);
        }

        /// <summary>
        /// 返回响应状态、消息、数据和跳转地址
        /// </summary>
        /// <param name="result">ResultInfo实体</param>
        /// <returns></returns>
        protected ContentResult Build(ResultInfo result)
        {
            var js = new ResultInfo(result.Status, result.Msg, result.Data);
            return Content(JsonConvert.SerializeObject(js, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }), "application/json", Encoding.UTF8);
        }

        /// <summary>
        /// 返回成功状态、消息和数据对象
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected ContentResult Yes(string msg, object data)
        {
            return Build(status: 1, msg: msg, data: data);
        }

        /// <summary>
        /// 返回成功状态和数据对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected ContentResult Yes(object data)
        {
            return Yes("Success", data: data);
        }

        /// <summary>
        /// 返回成功状态和消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        protected ContentResult Yes(string msg)
        {
            return Build(status: 1, msg: msg);
        }

        /// <summary>
        /// 返回成功状态
        /// </summary>
        /// <returns></returns>
        protected ContentResult Yes()
        {
            return Yes("Success");
        }

        /// <summary>
        /// 返回失败状态和消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        protected ContentResult No(string msg)
        {
            return Build(status: 0, msg: msg);
        }

        /// <summary>
        /// 返回失败状态
        /// </summary>
        /// <returns></returns>
        protected ContentResult No()
        {
            return No("Failure");
        }

        #endregion

        #region 通用返回ResultInfo的封装
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected ResultInfo InfoResp(int status, string msg)
        {
            return new ResultInfo(status, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected ResultInfo SuccResp(string msg)
        {
            return InfoResp(1, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected ResultInfo FailResp(string msg)
        {
            return InfoResp(0, msg);
        }
        #endregion

        #region 通用返回PageInfo的封装
        /// <summary>
        /// 分页响应数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        public ContentResult PageResult(object data, int pageCount, int totalCount)
        {
            return Content(JsonConvert.SerializeObject(new PageInfo(data, pageCount, totalCount), new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            }), "application/json", Encoding.UTF8);
        }
        #endregion

        #region Masuit返回结果json的封装
        /// <summary>
        /// 返回结果json
        /// </summary>
        /// <param name="data">响应数据</param>
        /// <param name="success">响应状态</param>
        /// <param name="message">响应消息</param>
        /// <param name="isLogin">登录状态</param>
        /// <returns></returns>
        public ActionResult ResultData(object data, bool success = true, string message = "", bool isLogin = true)
        {
            return Content(JsonConvert.SerializeObject(new
            {
                IsLogin = isLogin,
                Success = success,
                Message = message,
                Data = data
            }, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }), "application/json", Encoding.UTF8);
        }
        #endregion
    }
}