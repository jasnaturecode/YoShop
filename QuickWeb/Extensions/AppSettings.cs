using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace QuickWeb.Extensions
{
    /// <summary>
    /// appsetings.json 文件中的配置项
    /// </summary>
    public class AppSettings { }

    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class AppConfig : AppSettings
    {
        /// <summary>
        /// 调试模式
        /// </summary>
        public static bool IsDebug { get; set; }
        /// <summary>
        /// 百度密钥
        /// </summary>
        public static string BaiduAK { get; set; }
    }

    /// <summary>
    /// Redis配置文件
    /// </summary>
    public class RedisConfig : AppSettings
    {
        /// <summary>
        /// 是否启用Redis
        /// </summary>
        public static bool UseRedis { get; set; }

        /// <summary>
        /// 是否启用Redis缓存
        /// </summary>
        public static bool UseRedisCache { get; set; }

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

    }
}
