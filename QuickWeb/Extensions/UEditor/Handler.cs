using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace QuickWeb.Extensions.UEditor
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public abstract class Handler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected Handler(HttpContext context)
        {
            this.Request = context.Request;
            this.Response = context.Response;
            this.Context = context;
            //this.Server = context.Server;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract string Process();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected string WriteJson(object response)
        {
            string jsonpCallback = Request.Query["callback"];
            string json = JsonConvert.SerializeObject(response);
            return string.IsNullOrWhiteSpace(jsonpCallback) ? json : $"{jsonpCallback}({json});";
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpRequest Request { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpResponse Response { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpContext Context { get; private set; }
        //public HttpServerUtility Server { get; private set; }
    }
}