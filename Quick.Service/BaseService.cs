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

using Quick.Models.Entity.Table;

namespace Quick.Service
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseService<T>
    {

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
            return GetFirstEntity(u => u.user_name == username && u.password == password);
        }
    }

}
