/* ==============================================================================
* 命名空间：QuickWeb.Models.ViewModel 
* 类 名 称：DeliveryListViewModel
* 创 建 者：Run
* 创建时间：2019/4/14 0:45:14
* CLR 版本：4.0.30319.42000
* 保存的文件名：DeliveryListViewModel
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

using Quick.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickWeb.Models.ViewModel
{
    /// <summary>
    /// 运费模板列表
    /// </summary>
    public class DeliveryListViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DeliveryListViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="total"></param>
        public DeliveryListViewModel(IEnumerable<DeliveryDto> list, int total)
        {
            this.list = list;
            this.total = total;
        }


        /// <summary>
        /// 用户列表
        /// </summary>
        public IEnumerable<DeliveryDto> list { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; } = 0;

    }
}
