/* ==============================================================================
* 命名空间：QuickWeb.Extensions 
* 类 名 称：IConfigurationExtensions
* 创 建 者：Qing
* 创建时间：2019/04/10 14:42:06
* CLR 版本：4.0.30319.42000
* 保存的文件名：IConfigurationExtensions
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace QuickWeb.Extensions
{
    /// <summary>
    /// IConfiguration扩展方法
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 获取Config配置方法，只支持1级节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSectionValue<T>(this IConfiguration configuration) where T : AppSettings
        {
            var _ = configuration?.GetSection(typeof(T).Name);
            return _?.Get<T>();
        }

        /// <summary>
        /// 获取Config配置方法，最多支持2级节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="section">根节点</param>
        /// <param name="subsection">子节点</param>
        /// <returns></returns>
        public static T GetSectionValue<T>(this IConfiguration configuration, string section, string subsection = null) where T : class
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentNullException(nameof(section));
            var _ = configuration?.GetSection(section);
            return string.IsNullOrEmpty(subsection) ? _?.Value as T : _?.Get<T>();
        }
    }
}
