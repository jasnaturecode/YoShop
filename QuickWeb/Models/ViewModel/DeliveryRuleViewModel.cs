/* ==============================================================================
* 命名空间：QuickWeb.Models.ViewModel 
* 类 名 称：DeliveryRuleViewModel
* 创 建 者：Run
* 创建时间：2019/4/15 22:59:51
* CLR 版本：4.0.30319.42000
* 保存的文件名：DeliveryRuleViewModel
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
    /// <summary>
    /// 
    /// </summary>
    public class DeliveryRuleRegionViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string region_content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String region { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Double first { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal first_fee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Double additional { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal additional_fee { get; set; }

    }
}
