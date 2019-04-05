/* ==============================================================================
* 命名空间：Quick.Models.Dto 
* 类 名 称：MenuDto
* 创 建 者：Qing
* 创建时间：2019/04/03 20:15:52
* CLR 版本：4.0.30319.42000
* 保存的文件名：MenuDto
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
    public class MenuDto
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string index { get; set; }
        public MenuDto[] submenu { get; set; }
        public string color { get; set; }
        public bool is_svg { get; set; }
        public string[] uris { get; set; }
        public bool active { get; set; }
    }
}
