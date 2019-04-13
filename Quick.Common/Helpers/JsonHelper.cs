/* ==============================================================================
* 命名空间：Quick.Common.Helpers 
* 类 名 称：JsonHelper
* 创 建 者：Qing
* 创建时间：2019/04/13 17:58:01
* CLR 版本：4.0.30319.42000
* 保存的文件名：JsonHelper
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

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Helpers
{
    /// <summary>
    /// Json序列化帮助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="missMemeberIgnore">忽略丢失的属性</param>
        /// <param name="referenceLoopIgnore">忽略循环引用</param>
        /// <param name="propertyCamelCase">驼峰式命名：针对C#与Js命名方式不一致</param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json, bool missMemeberIgnore = true, bool referenceLoopIgnore = true, bool propertyCamelCase = true)
        {
            var setting = new JsonSerializerSettings();
            if (missMemeberIgnore)
                setting.MissingMemberHandling = MissingMemberHandling.Ignore;
            if (referenceLoopIgnore)
                setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            if (propertyCamelCase)
                setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="nullValueIgnore">忽略空值类型</param>
        /// <param name="propertyCamelCase">驼峰式命名：针对C#与Js命名方式不一致</param>
        /// <returns></returns>
        public static string ObjectToJson(this object obj, bool nullValueIgnore = true, bool propertyCamelCase = true)
        {
            var setting = new JsonSerializerSettings();
            if (nullValueIgnore)
                setting.NullValueHandling = NullValueHandling.Ignore;
            if (propertyCamelCase)
                setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            try
            {
                return JsonConvert.SerializeObject(obj, setting);
            }
            catch
            {
                return null;
            }
        }
    }
}
