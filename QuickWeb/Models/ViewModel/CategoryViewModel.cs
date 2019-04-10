/* ==============================================================================
* 命名空间：QuickWeb.Models.ViewModel 
* 类 名 称：CategoryViewModel
* 创 建 者：Qing
* 创建时间：2019/04/10 20:05:22
* CLR 版本：4.0.30319.42000
* 保存的文件名：CategoryViewModel
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

namespace QuickWeb.Models.ViewModel
{
    public class CategoryViewModel
    {
               /// <summary>
        /// 
        /// </summary>
        public uint category_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint parent_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint image_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime update_time { get; set; }
    }
}
