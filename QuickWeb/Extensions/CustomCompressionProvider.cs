/* ==============================================================================
* 命名空间：QuickWeb.Extensions 
* 类 名 称：CustomCompressionProvider
* 创 建 者：Run
* 创建时间：2019/4/13 10:39:58
* CLR 版本：4.0.30319.42000
* 保存的文件名：CustomCompressionProvider
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

using Microsoft.AspNetCore.ResponseCompression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickWeb.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomCompressionProvider : ICompressionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public string EncodingName => "mycustomcompression";
        /// <summary>
        /// 
        /// </summary>
        public bool SupportsFlush => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputStream"></param>
        /// <returns></returns>
        public Stream CreateStream(Stream outputStream)
        {
            // Create a custom compression stream wrapper here
            return outputStream;
        }
    }
}
