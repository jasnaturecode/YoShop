/* ==============================================================================
* 命名空间：Quick.Service 
* 类 名 称：BaseService
* 创 建 者：Qing
* 创建时间：2019-03-31 19:06:02
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseService
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

using Quick.Common;
using Quick.Models.Entity.Table;
using System;
using System.Security.Cryptography;

namespace Quick.Service
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseService<T>
    {
        /// <summary>
        /// 密码加密方案
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string source, string key = QuickKeys.EncryptKey)
        {
            return GetMD5(GetMD5(source) + key);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetMD5(string source)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //获取密文字节数组
            byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(source));
            //转换成字符串，32位
            string strResult = BitConverter.ToString(bytResult);
            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
            strResult = strResult.Replace("-", "");
            return strResult.ToLower();
        }
    }

    /// <summary>
    /// yoshop_user业务类
    /// </summary>
    public partial class yoshop_userService
    {

    }

    /// <summary>
    /// yoshop_store_user业务类
    /// </summary>
    public partial class yoshop_store_userService
    {
        /// <summary>
        /// 商家用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public yoshop_store_user Login(string username, string password)
        {
            var pwd = MD5Encrypt(password);
            return GetFirstEntity(u => u.user_name == username && u.password == pwd);
        }

        /// <summary>
        /// 商家用户修改密码
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ChangePwd(uint user_id, string password)
        {
            var pwd = MD5Encrypt(password);
            return Update(x => new yoshop_store_user { password = pwd }, l => l.store_user_id == user_id);
        }
    }

}
