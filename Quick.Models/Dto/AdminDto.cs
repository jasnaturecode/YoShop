/* ==============================================================================
* 命名空间：Quick.Models.Dto 
* 类 名 称：AdminDto
* 创 建 者：Qing
* 创建时间：2019/04/10 17:55:59
* CLR 版本：4.0.30319.42000
* 保存的文件名：AdminDto
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
using System.Text;

namespace Quick.Models.Dto
{
    public class AdminDto
    {
        /// <summary>
        /// 
        /// </summary>
        public uint store_user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint create_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 update_time { get; set; }
    }
}
