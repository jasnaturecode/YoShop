using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using QuickWeb.Extensions.Middleware;

namespace QuickWeb.Extensions
{
    /// <summary>
    /// IIApplicationBuilder扩展方法
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }

        /// <summary>
        /// 请求拦截
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestIntercept(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestInterceptMiddleware>();
        }
    }
}
