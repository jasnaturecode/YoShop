using System;

namespace Masuit.MyBlogs.Core.Extensions.Hangfire
{
    /// <summary>
    /// 
    /// </summary>
    public class IpIntercepter
    {
        /// <summary>
        /// 
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time { get; set; }
    }
}