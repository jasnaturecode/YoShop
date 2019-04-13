/* ==============================================================================
* 命名空间：Quick.Models.Dto 
* 类 名 称：CategoryDto
* 创 建 者：Qing
* 创建时间：2019/04/13 21:40:06
* CLR 版本：4.0.30319.42000
* 保存的文件名：CategoryDto
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

using Quick.Models.Entity.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Models.Dto
{
    public class CategoryDto : yoshop_category
    {
        /// <summary>
        /// 
        /// </summary>
        public new DateTime create_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public new DateTime update_time { get; set; }
    }
}
