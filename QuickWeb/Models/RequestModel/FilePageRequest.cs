/* ==============================================================================
* 命名空间：QuickWeb.Models.RequestModel 
* 类 名 称：LibraryFileRequest
* 创 建 者：Run
* 创建时间：2019/4/11 11:46:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：LibraryFileRequest
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

namespace QuickWeb.Models.RequestModel
{
    /// <summary>
    /// 分页操作
    /// </summary>
    public class FilePageRequest
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int current_page { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int last_page { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int per_page { get; set; } = 32;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int total  = 0;
        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }
    }
}
