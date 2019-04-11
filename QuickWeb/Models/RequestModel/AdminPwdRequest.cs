/* ==============================================================================
* 命名空间：QuickWeb.Models.RequestModel 
* 类 名 称：AdminPwdRequest
* 创 建 者：Run
* 创建时间：2019/4/12 1:34:47
* CLR 版本：4.0.30319.42000
* 保存的文件名：AdminPwdRequest
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
    /// 管理员密码
    /// </summary>
    public class AdminPwdRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string password_new { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string password_confirm { get; set; }

    }
}
