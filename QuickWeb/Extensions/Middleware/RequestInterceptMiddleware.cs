using System.Threading.Tasks;
using Masuit.Tools.Core.Net;
using Microsoft.AspNetCore.Http;
using QuickWeb.Extensions.Common;

namespace QuickWeb.Extensions.Middleware
{
    /// <summary>
    /// 请求拦截器
    /// </summary>
    public class RequestInterceptMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public RequestInterceptMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (!context.Session.TryGetValue("session", out _) && !context.Request.IsRobot())
            {
                context.Session.Set("session", 0);
                CommonHelper.InterviewCount++;
            }
            await _next.Invoke(context);
        }
    }
}