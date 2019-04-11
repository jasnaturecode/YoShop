/* ==============================================================================
* 命名空间：Quick.IService 
* 类 名 称：IBaseService
* 创 建 者：Qing
* 创建时间：2019-03-25 19:05:33
* CLR 版本：4.0.30319.42000
* 保存的文件名：IBaseService
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

namespace Quick.IService
{
    /// <summary>
    /// 业务层基类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IBaseService<T>
    {

    }

    /// <summary>
    /// yoshop_store_user业务接口
    /// </summary>
    public partial interface Iyoshop_store_userService
    {
        /// <summary>
        /// 商家用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        yoshop_store_user Login(string username, string password);
    }

    /// <summary>
    /// yoshop_user业务接口
    /// </summary>
    public partial interface Iyoshop_userService
    {

    }
}
