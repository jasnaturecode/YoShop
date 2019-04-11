/* ==============================================================================
* 命名空间：QuickWeb.Models.RequestModel 
* 类 名 称：UploadGroupRequest
* 创 建 者：Run
* 创建时间：2019/4/11 15:19:48
* CLR 版本：4.0.30319.42000
* 保存的文件名：UploadGroupRequest
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
    /// 上传文件分组
    /// </summary>
    public class UploadGroupRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public uint group_id { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 分组类别
        /// </summary>
        public string group_type { get; set; }
    }
}
