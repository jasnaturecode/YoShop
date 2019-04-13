/* ==============================================================================
* 命名空间：Quick.Models.Dto 
* 类 名 称：RegionDto
* 创 建 者：Run
* 创建时间：2019/4/14 1:39:23
* CLR 版本：4.0.30319.42000
* 保存的文件名：RegionDto
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
    public class RegionDto
    {
        public System.Int32 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte? level { get; set; }
    }
}
