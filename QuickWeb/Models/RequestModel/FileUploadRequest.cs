/* ==============================================================================
* 命名空间：QuickWeb.Models.RequestModel 
* 类 名 称：FileUploadRequest
* 创 建 者：Run
* 创建时间：2019/4/12 10:17:13
* CLR 版本：4.0.30319.42000
* 保存的文件名：FileUploadRequest
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
    /// 文件上传
    /// </summary>
    public class FileUploadRequest
    {
        /// <summary>
        /// 上传Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 上传文件名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 文件分组
        /// </summary>
        public uint group_id { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime lastModifiedDate { get; set; }
        /// <summary>
        /// 上传文件大小
        /// </summary>
        public uint size { get; set; }
    }
}
