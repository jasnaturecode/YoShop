﻿ 
/* ==============================================================================
* 命名空间：Quick.IService
* 类 名 称：IBaseService
* 创 建 者：Qing
* 创建时间：2019-03-31 17:45:11
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
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quick.IService
{
    public partial interface IBaseService<T>
    {
        #region 基本增删改
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="t">需要添加的实体</param>
        /// <returns>添加成功</returns>
        bool AddEntity(T t);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="t">需要添加</param>
        /// <returns>添加成功的实体ID</returns>
        int AddEntityReturnIdentity(T t);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="list">需要添加的实体</param>
        /// <returns>添加成功</returns>
        bool AddEntities(IEnumerable<T> list);

        /// <summary>
        /// 根据ID删除实体（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>删除成功</returns>
        bool DeleteById(object id);

        /// <summary>
        /// 根据ID集合删除实体（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件
        /// </summary>
        /// <param name="ids">实体ids集合</param>
        /// <returns>删除成功</returns>
        bool DeleteByIds(object[] ids);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="t">需要删除的实体</param>
        /// <returns>删除成功</returns>
        bool DeleteEntity(T t);

        /// <summary>
        /// 根据条件批量删除实体
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>删除成功</returns>
        bool Delete(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="t">更新后的实体</param>
        /// <returns>更新成功</returns>
        bool UpdateEntity(T t);

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="list">更新后的实体</param>
        /// <returns>更新成功</returns>
        bool UpdateEntities(IEnumerable<T> list);

        /// <summary>
        /// 根据条件更新实体
        /// </summary>
        /// <param name="columns">待更新的实体属性</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        bool Update(Expression<Func<T, T>> @columns, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 执行DML语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        int ExecuteSql(string sql, params SqlParameter[] parameters);

		/// <summary>
        /// 获取DataTable数据
        /// </summary>
        /// <param name="sql">待执行的sql语句</param>
        /// <param name="parameters">预编译的参数</param>
        /// <returns></returns>
        DataTable GetDataTable(string sql,object parameters = null);

		/// <summary>
        /// 获取数据的记录数
        /// </summary>
        /// <param name="sql">待执行的sql语句</param>
        /// <param name="parameters">预编译的参数</param>
        /// <returns></returns>
        int GetCount(string sql,object parameters = null);

        /// <summary>
        /// 判断实体是否在数据库中存在
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否存在</returns>
        bool Any(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 符合条件的个数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否存在</returns>
        int Count(Expression<Func<T, bool>> @where);

        #endregion

        #region 简单、高级查询
        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>还未执行的SQL语句</returns>
        ISugarQueryable<T> GetAll();

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        ISugarQueryable<T> GetAll<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="thenby">次排序</param>
        /// <returns>还未执行的SQL语句</returns>
        ISugarQueryable<T> GetAll<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, params Expression<Func<T, object>>[] thenby);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        IEnumerable<T> GetAllFromCache<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="thenby">次排序</param>
        /// <returns>还未执行的SQL语句</returns>
        IEnumerable<T> GetAllFromCache<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20, params Expression<Func<T, object>>[] thenby);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> GetAllFromCacheAsync<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="thenby">次排序</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> GetAllFromCacheAsync<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20, params Expression<Func<T, object>>[] thenby);

        /// <summary>
        /// 基本查询方法，获取一个集合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>还未执行的SQL语句</returns>
        ISugarQueryable<T> LoadEntities(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 基本查询方法，获取一个集合
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        ISugarQueryable<T> LoadOrderedEntities<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true);

        /// <summary>
        /// 基本查询方法，获取一个集合
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        ISugarQueryable<T> LoadOrderedEntities<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, params Expression<Func<T, object>>[] thenby);

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        IEnumerable<T> LoadEntitiesFromCache(Expression<Func<T, bool>> @where, int timespan = 30);

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IEnumerable<T> LoadEntitiesFromCache<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30);

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IEnumerable<T> LoadEntitiesFromCache<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30, params Expression<Func<T, object>>[] thenby);

        /// <summary>
        /// 基本查询方法，获取一个被AutoMapper映射后的集合(异步)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> LoadEntitiesAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 基本查询方法，获取一个被AutoMapper映射后的集合(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> LoadEntitiesAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true);

        /// <summary>
        /// 基本查询方法，获取一个被AutoMapper映射后的集合(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> LoadEntitiesAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, params Expression<Func<T, object>>[] thenby);

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取(异步)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> LoadEntitiesFromCacheAsync(Expression<Func<T, bool>> @where, int timespan = 30);

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> LoadEntitiesFromCacheAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30);

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        Task<IEnumerable<T>> LoadEntitiesFromCacheAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30, params Expression<Func<T, object>>[] thenby);

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>实体</returns>
        T GetFirstEntity(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体</returns>
        T GetFirstEntity<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true);

        /// <summary>
        /// 获取第一条数据，优先从缓存读取
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>实体</returns>
        T GetFirstEntityFromCache(Expression<Func<T, bool>> @where, int timespan = 30);

        /// <summary>
        /// 获取第一条数据，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>映射实体</returns>
        T GetFirstEntityFromCache<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30);

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>实体</returns>
        Task<T> GetFirstEntityAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体</returns>
        Task<T> GetFirstEntityAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true);

        /// <summary>
        /// 获取第一条数据，优先从缓存读取(异步)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>实体</returns>
        Task<T> GetFirstEntityFromCacheAsync(Expression<Func<T, bool>> @where, int timespan = 30);

        /// <summary>
        /// 获取第一条数据，优先从缓存读取(异步)
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>实体</returns>
        Task<T> GetFirstEntityFromCacheAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30);

        /// <summary>
        /// 根据ID找实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        T GetById(object id);

        /// <summary>
        /// 高效分页查询方法
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="where">where Lambda条件表达式</param>
        /// <param name="orderby">orderby Lambda条件表达式</param>
        /// <param name="isAsc">升序降序</param>
        /// <returns>还未执行的SQL语句</returns>
        IEnumerable<T> LoadPageEntities<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby, bool isAsc);

        /// <summary>
        /// 高效分页查询方法
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="where">where Lambda条件表达式</param>
        /// <param name="orderby">orderby Lambda条件表达式</param>
        /// <param name="isAsc">升序降序</param>
        /// <returns>还未执行的SQL语句</returns>
        IEnumerable<T> LoadPageEntities<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby, bool isAsc, params Expression<Func<T, object>>[] thenby);
        
        /// <summary>
        /// 高效分页查询方法，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="where">where Lambda条件表达式</param>
        /// <param name="orderby">orderby Lambda条件表达式</param>
        /// <param name="isAsc">升序降序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        IEnumerable<T> LoadPageEntitiesFromCache<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby, bool isAsc, int timespan = 30);

        /// <summary>
        /// 高效分页查询方法，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="where">where Lambda条件表达式</param>
        /// <param name="orderby">orderby Lambda条件表达式</param>
        /// <param name="isAsc">升序降序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        IEnumerable<T> LoadPageEntitiesFromCache<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby, bool isAsc, int timespan = 30, params Expression<Func<T, object>>[] thenby);
        
        #endregion
    }

    /// <summary>
    /// yoshop_category业务接口
    /// </summary>
	public partial interface Iyoshop_categoryService : IBaseService<yoshop_category>
	{
		
	}

    /// <summary>
    /// yoshop_delivery业务接口
    /// </summary>
	public partial interface Iyoshop_deliveryService : IBaseService<yoshop_delivery>
	{
		
	}

    /// <summary>
    /// yoshop_delivery_rule业务接口
    /// </summary>
	public partial interface Iyoshop_delivery_ruleService : IBaseService<yoshop_delivery_rule>
	{
		
	}

    /// <summary>
    /// yoshop_dictionary业务接口
    /// </summary>
	public partial interface Iyoshop_dictionaryService : IBaseService<yoshop_dictionary>
	{
		
	}

    /// <summary>
    /// yoshop_goods业务接口
    /// </summary>
	public partial interface Iyoshop_goodsService : IBaseService<yoshop_goods>
	{
		
	}

    /// <summary>
    /// yoshop_goods_image业务接口
    /// </summary>
	public partial interface Iyoshop_goods_imageService : IBaseService<yoshop_goods_image>
	{
		
	}

    /// <summary>
    /// yoshop_goods_spec业务接口
    /// </summary>
	public partial interface Iyoshop_goods_specService : IBaseService<yoshop_goods_spec>
	{
		
	}

    /// <summary>
    /// yoshop_goods_spec_rel业务接口
    /// </summary>
	public partial interface Iyoshop_goods_spec_relService : IBaseService<yoshop_goods_spec_rel>
	{
		
	}

    /// <summary>
    /// yoshop_order业务接口
    /// </summary>
	public partial interface Iyoshop_orderService : IBaseService<yoshop_order>
	{
		
	}

    /// <summary>
    /// yoshop_order_address业务接口
    /// </summary>
	public partial interface Iyoshop_order_addressService : IBaseService<yoshop_order_address>
	{
		
	}

    /// <summary>
    /// yoshop_order_goods业务接口
    /// </summary>
	public partial interface Iyoshop_order_goodsService : IBaseService<yoshop_order_goods>
	{
		
	}

    /// <summary>
    /// yoshop_region业务接口
    /// </summary>
	public partial interface Iyoshop_regionService : IBaseService<yoshop_region>
	{
		
	}

    /// <summary>
    /// yoshop_setting业务接口
    /// </summary>
	public partial interface Iyoshop_settingService : IBaseService<yoshop_setting>
	{
		
	}

    /// <summary>
    /// yoshop_spec业务接口
    /// </summary>
	public partial interface Iyoshop_specService : IBaseService<yoshop_spec>
	{
		
	}

    /// <summary>
    /// yoshop_spec_value业务接口
    /// </summary>
	public partial interface Iyoshop_spec_valueService : IBaseService<yoshop_spec_value>
	{
		
	}

    /// <summary>
    /// yoshop_store_user业务接口
    /// </summary>
	public partial interface Iyoshop_store_userService : IBaseService<yoshop_store_user>
	{
		
	}

    /// <summary>
    /// yoshop_upload_file业务接口
    /// </summary>
	public partial interface Iyoshop_upload_fileService : IBaseService<yoshop_upload_file>
	{
		
	}

    /// <summary>
    /// yoshop_upload_file_used业务接口
    /// </summary>
	public partial interface Iyoshop_upload_file_usedService : IBaseService<yoshop_upload_file_used>
	{
		
	}

    /// <summary>
    /// yoshop_upload_group业务接口
    /// </summary>
	public partial interface Iyoshop_upload_groupService : IBaseService<yoshop_upload_group>
	{
		
	}

    /// <summary>
    /// yoshop_user业务接口
    /// </summary>
	public partial interface Iyoshop_userService : IBaseService<yoshop_user>
	{
		
	}

    /// <summary>
    /// yoshop_user_address业务接口
    /// </summary>
	public partial interface Iyoshop_user_addressService : IBaseService<yoshop_user_address>
	{
		
	}

    /// <summary>
    /// yoshop_wxapp业务接口
    /// </summary>
	public partial interface Iyoshop_wxappService : IBaseService<yoshop_wxapp>
	{
		
	}

    /// <summary>
    /// yoshop_wxapp_help业务接口
    /// </summary>
	public partial interface Iyoshop_wxapp_helpService : IBaseService<yoshop_wxapp_help>
	{
		
	}

    /// <summary>
    /// yoshop_wxapp_navbar业务接口
    /// </summary>
	public partial interface Iyoshop_wxapp_navbarService : IBaseService<yoshop_wxapp_navbar>
	{
		
	}

    /// <summary>
    /// yoshop_wxapp_page业务接口
    /// </summary>
	public partial interface Iyoshop_wxapp_pageService : IBaseService<yoshop_wxapp_page>
	{
		
	}


}
