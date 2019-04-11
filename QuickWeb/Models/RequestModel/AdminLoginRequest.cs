/* ==============================================================================
* 命名空间：QuickWeb.Models.RequestModel 
* 类 名 称：AdminLoginRequest
* 创 建 者：Qing
* 创建时间：2019/04/11 22:26:22
* CLR 版本：4.0.30319.42000
* 保存的文件名：AdminLoginRequest
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
    /// 商家登录请求
    /// </summary>
    public class AdminLoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
