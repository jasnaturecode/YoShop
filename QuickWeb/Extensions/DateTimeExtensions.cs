/* ==============================================================================
* 命名空间：QuickWeb.Extensions 
* 类 名 称：DateTimeExtensions
* 创 建 者：Qing
* 创建时间：2019/04/10 20:02:24
* CLR 版本：4.0.30319.42000
* 保存的文件名：DateTimeExtensions
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

namespace QuickWeb.Extensions
{
    /// <summary>
    /// 时间转换扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static uint ConvertToTimeStamp(this DateTime dt)
        {
            var _ = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (uint)(dt.AddHours(-8) - _).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this uint ts)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(ts).AddHours(8);
        }
    }
}
