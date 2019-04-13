/* ==============================================================================
* 命名空间：QuickWeb.Models.ViewModel 
* 类 名 称：UserListViewModel
* 创 建 者：Qing
* 创建时间：2019/04/13 17:05:45
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserListViewModel
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
using Quick.Models.Entity.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickWeb.Models.ViewModel
{
    /// <summary>
    /// 用户列表视图
    /// </summary>
    public class UserListViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public UserListViewModel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="total"></param>
        public UserListViewModel(IEnumerable<UserDto> list, int total)
        {
            this.list = list;
            this.total = total;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        public IEnumerable<UserDto> list { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; } = 0;
    }
}
