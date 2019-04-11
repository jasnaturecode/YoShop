/* ==============================================================================
* 命名空间：Quick.Common 
* 类 名 称：QuickKeys
* 创 建 者：Qing
* 创建时间：2019/04/10 18:07:30
* CLR 版本：4.0.30319.42000
* 保存的文件名：QuickKeys
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

namespace Quick.Common
{
    /// <summary>
    /// 系统配置Key集合
    /// </summary>
    public class QuickKeys
    {
        /// <summary>
        /// 管理员登录地址
        /// </summary>
        public const string AdminLogin = "/passport/login";
        /// <summary>
        /// 管理员中心地址
        /// </summary>
        public const string AdminHome = "/home/index";

        /// <summary>
        ///  管理员密码密钥
        /// </summary>
        public const string EncryptKey = "yoshop_salt_SmTRx";

    }

    /// <summary>
    /// SessionKey
    /// </summary>
    public class SessionKey
    {
        /// <summary>
        /// 管理员SessionKey
        /// </summary>
        public const string AdminInfo = "AdminSession";
        /// <summary>
        /// 用户SessionKey
        /// </summary>
        public const string UserInfo = "UserSession";
    }
}
