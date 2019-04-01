using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace QuickWeb.Extensions
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// 百度密钥
        /// </summary>
        public static string BaiduAk { get; set; }

        /// <summary>
        /// 程序名称
        /// </summary>
        public static string AppTitle { get; set; }

        /// <summary>
        /// 程序版本
        /// </summary>
        public static string AppVersion { get; set; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public static void Init(IConfiguration configuration)
        {
            AppTitle = configuration[nameof(AppTitle)];
            AppVersion = configuration[nameof(AppVersion)];
            BaiduAk = configuration[nameof(BaiduAk)];
        }
    }
}
