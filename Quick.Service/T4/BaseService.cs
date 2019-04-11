
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

using Quick.IService;
using Quick.Models.Application;
using Quick.Models.Entity.Table;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quick.Service
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseService<T> : DbContext, IBaseService<T> where T : class, new()
    {
        #region 基本增删改

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="t">需要添加的实体</param>
        /// <returns>添加成功</returns>
        public bool AddEntity(T t)
        {
            return Db.GetSimpleClient().Insert(t);
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="t">需要添加</param>
        /// <returns>添加成功的实体ID</returns>
        public int AddEntityReturnIdentity(T t)
        {
            return Db.GetSimpleClient().InsertReturnIdentity(t);
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="list">需要添加的实体</param>
        /// <returns>添加成功</returns>
        public bool AddEntities(IEnumerable<T> list)
        {
            return Db.GetSimpleClient().InsertRange(list.ToArray());
        }
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>删除成功</returns>
        public bool DeleteById(object id)
        {
            return Db.GetSimpleClient().DeleteById<object>(id);
        }

        /// <summary>
        /// 根据ID集合删除实体
        /// </summary>
        /// <param name="ids">实体ids集合</param>
        /// <returns>删除成功</returns>
        public bool DeleteByIds(object[] ids)
        {
            return Db.GetSimpleClient().DeleteByIds<object>(ids);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="t">需要删除的实体</param>
        /// <returns>删除成功</returns>
        public bool DeleteEntity(T t)
        {
            return Db.GetSimpleClient().Delete(t);
        }

        /// <summary>
        /// 根据条件批量删除实体
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>删除成功</returns>
        public bool Delete(Expression<Func<T, bool>> @where)
        {
            return Db.GetSimpleClient().Delete(where);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="t">更新后的实体</param>
        /// <returns>更新成功</returns>
        public bool UpdateEntity(T t)
        {
            return Db.GetSimpleClient().Update(t);
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="list">更新后的实体</param>
        /// <returns>更新成功</returns>
        public bool UpdateEntities(IEnumerable<T> list)
        {
            return Db.GetSimpleClient().UpdateRange(list.ToArray());
        }

        /// <summary>
        /// 根据条件更新实体
        /// </summary>
        /// <param name="columns">待更新的实体属性</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public bool Update(Expression<Func<T, T>> @columns, Expression<Func<T, bool>> @where)
        {
            return Db.GetSimpleClient().Update(@columns, @where);
        }

        /// <summary>
        /// 判断实体是否在数据库中存在
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否存在</returns>
        public bool Any(Expression<Func<T, bool>> @where)
        {
            //return Db.GetSimpleClient().IsAny(@where);
            return Db.Queryable<T>().Any(@where);
        }

        /// <summary>
        /// 符合条件的个数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否存在</returns>
        public int Count(Expression<Func<T, bool>> @where)
        {
            return Db.Queryable<T>().Count(@where);
        }

        /// <summary>
        /// 执行DML语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public int ExecuteSql(string sql, params SqlParameter[] parameters)
        {
            return parameters == null ? Db.Ado.ExecuteCommand(sql) : Db.Ado.ExecuteCommand(sql, parameters);
        }

        /// <summary>
        /// 获取DataTable数据
        /// </summary>
        /// <param name="sql">待执行的sql语句</param>
        /// <param name="parameters">预编译的参数</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, object parameters = null)
        {
            return parameters == null ? Db.Ado.GetDataTable(sql) : Db.Ado.GetDataTable(sql, parameters);
        }

        /// <summary>
        /// 获取数据的记录数
        /// </summary>
        /// <param name="sql">待执行的sql语句</param>
        /// <param name="parameters">预编译的参数</param>
        /// <returns></returns>
        public int GetCount(string sql, object parameters = null)
        {
            return parameters == null ? Db.Ado.GetInt(sql) : Db.Ado.GetInt(sql, parameters);
        }

        #endregion

        #region 简单、高级查询

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>还未执行的SQL语句</returns>
        public ISugarQueryable<T> GetAll()
        {
            return Db.Queryable<T>();
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        public ISugarQueryable<T> GetAll<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true)
        {
            return Db.Queryable<T>().OrderBy(@orderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="thenby">次排序</param>
        /// <returns>还未执行的SQL语句</returns>
        public ISugarQueryable<T> GetAll<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, params Expression<Func<T, object>>[] thenby)
        {
            var query = Db.Queryable<T>().OrderBy(@orderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
            if (thenby.Length > 0)
            {
                query = thenby.Aggregate(query, (current, e) => current.OrderBy(e, isAsc ? OrderByType.Asc : OrderByType.Desc));
            }
            return query;
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        public IEnumerable<T> GetAllFromCache<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20)
        {
            return GetAll<TS>(@orderby, isAsc).WithCache(timespan).ToList();
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="thenby">次排序</param>
        /// <returns>还未执行的SQL语句</returns>
        public IEnumerable<T> GetAllFromCache<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20, params Expression<Func<T, object>>[] thenby)
        {
            return GetAll<TS>(@orderby, isAsc, thenby).WithCache(timespan).ToList();
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> GetAllFromCacheAsync<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20)
        {
            return await GetAll<TS>(@orderby, isAsc).WithCache(timespan).ToListAsync();
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="thenby">次排序</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> GetAllFromCacheAsync<TS>(Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 20, params Expression<Func<T, object>>[] thenby)
        {
            return await GetAll<TS>(@orderby, isAsc, thenby).WithCache(timespan).ToListAsync();
        }

        /// <summary>
        /// 基本查询方法，获取一个集合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>还未执行的SQL语句</returns>
        public ISugarQueryable<T> LoadEntities(Expression<Func<T, bool>> @where)
        {
            return Db.Queryable<T>().Where(@where);
        }

        /// <summary>
        /// 基本查询方法，获取一个集合
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        public ISugarQueryable<T> LoadOrderedEntities<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true)
        {
            return LoadEntities(@where).OrderBy(@orderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
        }

        /// <summary>
        /// 基本查询方法，获取一个集合
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        public ISugarQueryable<T> LoadOrderedEntities<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true,
            params Expression<Func<T, object>>[] thenby)
        {
            var query = LoadEntities(@where).OrderBy(@orderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
            if (thenby.Length > 0)
            {
                query = thenby.Aggregate(query, (current, e) => current.OrderBy(e, isAsc ? OrderByType.Asc : OrderByType.Desc));
            }
            return query;
        }

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        public IEnumerable<T> LoadEntitiesFromCache(Expression<Func<T, bool>> @where, int timespan = 30)
        {
            return LoadEntities(@where).WithCache(timespan).ToList();
        }

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public IEnumerable<T> LoadEntitiesFromCache<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30)
        {
            return LoadOrderedEntities<TS>(@where, @orderby, isAsc).WithCache(timespan).ToList();
        }

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public IEnumerable<T> LoadEntitiesFromCache<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30,
            params Expression<Func<T, object>>[] thenby)
        {
            return LoadOrderedEntities<TS>(@where, @orderby, isAsc, thenby).WithCache(timespan).ToList();
        }

        /// <summary>
        /// 基本查询方法，获取一个被AutoMapper映射后的集合(异步)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> LoadEntitiesAsync(Expression<Func<T, bool>> @where)
        {
            return await LoadEntities(@where).ToListAsync();
        }

        /// <summary>
        /// 基本查询方法，获取一个被AutoMapper映射后的集合(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> LoadEntitiesAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true)
        {
            return await LoadOrderedEntities<TS>(@where, @orderby, isAsc).ToListAsync();
        }

        /// <summary>
        /// 基本查询方法，获取一个被AutoMapper映射后的集合(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> LoadEntitiesAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, params Expression<Func<T, object>>[] thenby)
        {
            return await LoadOrderedEntities<TS>(@where, @orderby, isAsc, thenby).ToListAsync();
        }

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取(异步)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> LoadEntitiesFromCacheAsync(Expression<Func<T, bool>> @where, int timespan = 30)
        {
            return await LoadEntities(@where).WithCache(timespan).ToListAsync();
        }

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> LoadEntitiesFromCacheAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30)
        {
            return await LoadOrderedEntities<TS>(@where, @orderby, isAsc).WithCache(timespan).ToListAsync();
        }

        /// <summary>
        /// 基本查询方法，获取一个集合，优先从缓存读取(异步)
        /// </summary>
        /// <typeparam name="TS">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>还未执行的SQL语句</returns>
        public virtual async Task<IEnumerable<T>> LoadEntitiesFromCacheAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30,
            params Expression<Func<T, object>>[] thenby)
        {
            return await LoadOrderedEntities<TS>(@where, @orderby, isAsc, thenby).WithCache(timespan).ToListAsync();
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>实体</returns>
        public T GetFirstEntity(Expression<Func<T, bool>> @where)
        {
            return LoadEntities(@where).First();
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体</returns>
        public T GetFirstEntity<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true)
        {
            return LoadOrderedEntities<TS>(@where, @orderby, isAsc).First();
        }

        /// <summary>
        /// 获取第一条数据，优先从缓存读取
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>实体</returns>
        public T GetFirstEntityFromCache(Expression<Func<T, bool>> @where, int timespan = 30)
        {
            return LoadEntities(@where).WithCache(timespan).First();
        }

        /// <summary>
        /// 获取第一条数据，优先从缓存读取
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>映射实体</returns>
        public T GetFirstEntityFromCache<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30)
        {
            return LoadEntities(@where).WithCache(timespan).First();
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>实体</returns>
        public virtual async Task<T> GetFirstEntityAsync(Expression<Func<T, bool>> @where)
        {
            return await LoadEntities(@where).FirstAsync();
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体</returns>
        public virtual async Task<T> GetFirstEntityAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true)
        {
            return await LoadOrderedEntities<TS>(@where, @orderby, isAsc).FirstAsync();
        }

        /// <summary>
        /// 获取第一条数据，优先从缓存读取(异步)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>实体</returns>
        public virtual async Task<T> GetFirstEntityFromCacheAsync(Expression<Func<T, bool>> @where, int timespan = 30)
        {
            return await LoadEntities(@where).WithCache(timespan).FirstAsync();
        }

        /// <summary>
        /// 获取第一条数据，优先从缓存读取(异步)
        /// </summary>
        /// <typeparam name="TS">排序</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns>实体</returns>
        public virtual async Task<T> GetFirstEntityFromCacheAsync<TS>(Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby, bool isAsc = true, int timespan = 30)
        {
            return await LoadOrderedEntities<TS>(@where, @orderby, isAsc).WithCache(timespan).FirstAsync();
        }

        /// <summary>
        /// 根据ID找实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public T GetById(object id)
        {
            return Db.Queryable<T>().InSingle(id);
        }

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
        public IEnumerable<T> LoadPageEntities<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby,
            bool isAsc)
        {
            return LoadOrderedEntities<TS>(@where, @orderby, isAsc).ToPageList(pageIndex, pageSize, ref totalCount);
        }

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
        public IEnumerable<T> LoadPageEntities<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> @where, Expression<Func<T, object>> @orderby,
            bool isAsc, params Expression<Func<T, object>>[] thenby)
        {
            return LoadOrderedEntities<TS>(@where, @orderby, isAsc, thenby).ToPageList(pageIndex, pageSize, ref totalCount);
        }

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
        public IEnumerable<T> LoadPageEntitiesFromCache<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> @where,
            Expression<Func<T, object>> @orderby, bool isAsc, int timespan = 30)
        {
            return LoadOrderedEntities<TS>(@where, @orderby, isAsc).WithCache(timespan).ToPageList(pageIndex, pageSize, ref totalCount);
        }

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
        public IEnumerable<T> LoadPageEntitiesFromCache<TS>(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> @where,
            Expression<Func<T, object>> @orderby, bool isAsc, int timespan = 30, params Expression<Func<T, object>>[] thenby)
        {
            return LoadOrderedEntities<TS>(@where, @orderby, isAsc, thenby).WithCache(timespan).ToPageList(pageIndex, pageSize, ref totalCount);
        }

        #endregion
    }

    /// <summary>
    /// yoshop_category业务类
    /// </summary>
	public partial class yoshop_categoryService : BaseService<yoshop_category>, Iyoshop_categoryService
    {

    }

    /// <summary>
    /// yoshop_delivery业务类
    /// </summary>
	public partial class yoshop_deliveryService : BaseService<yoshop_delivery>, Iyoshop_deliveryService
    {

    }

    /// <summary>
    /// yoshop_delivery_rule业务类
    /// </summary>
	public partial class yoshop_delivery_ruleService : BaseService<yoshop_delivery_rule>, Iyoshop_delivery_ruleService
    {

    }

    /// <summary>
    /// yoshop_dictionary业务类
    /// </summary>
	public partial class yoshop_dictionaryService : BaseService<yoshop_dictionary>, Iyoshop_dictionaryService
    {

    }

    /// <summary>
    /// yoshop_goods业务类
    /// </summary>
	public partial class yoshop_goodsService : BaseService<yoshop_goods>, Iyoshop_goodsService
    {

    }

    /// <summary>
    /// yoshop_goods_image业务类
    /// </summary>
	public partial class yoshop_goods_imageService : BaseService<yoshop_goods_image>, Iyoshop_goods_imageService
    {

    }

    /// <summary>
    /// yoshop_goods_spec业务类
    /// </summary>
	public partial class yoshop_goods_specService : BaseService<yoshop_goods_spec>, Iyoshop_goods_specService
    {

    }

    /// <summary>
    /// yoshop_goods_spec_rel业务类
    /// </summary>
	public partial class yoshop_goods_spec_relService : BaseService<yoshop_goods_spec_rel>, Iyoshop_goods_spec_relService
    {

    }

    /// <summary>
    /// yoshop_order业务类
    /// </summary>
	public partial class yoshop_orderService : BaseService<yoshop_order>, Iyoshop_orderService
    {

    }

    /// <summary>
    /// yoshop_order_address业务类
    /// </summary>
	public partial class yoshop_order_addressService : BaseService<yoshop_order_address>, Iyoshop_order_addressService
    {

    }

    /// <summary>
    /// yoshop_order_goods业务类
    /// </summary>
	public partial class yoshop_order_goodsService : BaseService<yoshop_order_goods>, Iyoshop_order_goodsService
    {

    }

    /// <summary>
    /// yoshop_region业务类
    /// </summary>
	public partial class yoshop_regionService : BaseService<yoshop_region>, Iyoshop_regionService
    {

    }

    /// <summary>
    /// yoshop_setting业务类
    /// </summary>
	public partial class yoshop_settingService : BaseService<yoshop_setting>, Iyoshop_settingService
    {

    }

    /// <summary>
    /// yoshop_spec业务类
    /// </summary>
	public partial class yoshop_specService : BaseService<yoshop_spec>, Iyoshop_specService
    {

    }

    /// <summary>
    /// yoshop_spec_value业务类
    /// </summary>
	public partial class yoshop_spec_valueService : BaseService<yoshop_spec_value>, Iyoshop_spec_valueService
    {

    }

    /// <summary>
    /// yoshop_store_user业务类
    /// </summary>
	public partial class yoshop_store_userService : BaseService<yoshop_store_user>, Iyoshop_store_userService
    {

    }

    /// <summary>
    /// yoshop_upload_file业务类
    /// </summary>
	public partial class yoshop_upload_fileService : BaseService<yoshop_upload_file>, Iyoshop_upload_fileService
    {

    }

    /// <summary>
    /// yoshop_upload_file_used业务类
    /// </summary>
	public partial class yoshop_upload_file_usedService : BaseService<yoshop_upload_file_used>, Iyoshop_upload_file_usedService
    {

    }

    /// <summary>
    /// yoshop_upload_group业务类
    /// </summary>
	public partial class yoshop_upload_groupService : BaseService<yoshop_upload_group>, Iyoshop_upload_groupService
    {

    }

    /// <summary>
    /// yoshop_user业务类
    /// </summary>
	public partial class yoshop_userService : BaseService<yoshop_user>, Iyoshop_userService
    {

    }

    /// <summary>
    /// yoshop_user_address业务类
    /// </summary>
	public partial class yoshop_user_addressService : BaseService<yoshop_user_address>, Iyoshop_user_addressService
    {

    }

    /// <summary>
    /// yoshop_wxapp业务类
    /// </summary>
	public partial class yoshop_wxappService : BaseService<yoshop_wxapp>, Iyoshop_wxappService
    {

    }

    /// <summary>
    /// yoshop_wxapp_help业务类
    /// </summary>
	public partial class yoshop_wxapp_helpService : BaseService<yoshop_wxapp_help>, Iyoshop_wxapp_helpService
    {

    }

    /// <summary>
    /// yoshop_wxapp_navbar业务类
    /// </summary>
	public partial class yoshop_wxapp_navbarService : BaseService<yoshop_wxapp_navbar>, Iyoshop_wxapp_navbarService
    {

    }

    /// <summary>
    /// yoshop_wxapp_page业务类
    /// </summary>
	public partial class yoshop_wxapp_pageService : BaseService<yoshop_wxapp_page>, Iyoshop_wxapp_pageService
    {

    }


}
