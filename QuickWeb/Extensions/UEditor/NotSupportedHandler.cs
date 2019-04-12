using Microsoft.AspNetCore.Http;

namespace QuickWeb.Extensions.UEditor
{
    /// <summary>
    /// NotSupportedHandler 的摘要说明
    /// </summary>
    public class NotSupportedHandler : Handler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public NotSupportedHandler(HttpContext context) : base(context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Process()
        {
            return WriteJson(new
            {
                state = "action 参数为空或者 action 不被支持。"
            });
        }
    }
}