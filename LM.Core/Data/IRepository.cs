// -----------------------------------------------------------------------
//  <copyright file="IRepository.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-02-06 15:46</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;




namespace LM.Core.Data
{
    /// <summary>
    /// 实体仓储模型的数据标准操作
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface IRepository<T> where T : class

    {
        #region 属性

       

        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// </summary>
        IQueryable<T> Table { get; set; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// </summary>
        IQueryable<T> TableNoTracking { get; set; }

        #endregion

       

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        void Add(T entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        void Add(IEnumerable<T> entities);





        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        void Delete(T entity);

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>操作影响的行数</returns>
        void Delete(object key);

        /// <summary>
        /// 删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        void Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        void Delete(IEnumerable<T> entities);





        /// <summary>
        /// 直接删除所有符合特定条件的实体，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        void DeleteDirect(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        void Update(T entity);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void Update(IEnumerable<T> entities);


        /// <summary>
        /// 直接更新指定条件的数据，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        int UpdateDirect(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatExpression);

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>是否存在</returns>
        bool Any(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        T GetByKey(object key);

      

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        IQueryable<T> GetInclude<TProperty>(Expression<Func<T, TProperty>> path);

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        IQueryable<T> GetIncludes(params string[] paths);

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集,不追踪
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        IQueryable<T> GetIncludesNoTracking(params string[] paths);


        /// <summary>
        /// 高速批量插入数据,不需要Save
        /// </summary>
        /// <param name="entities"></param>
        void BulkInsert(IEnumerable<T> entities);

        /// <summary>
        ///     判断是否所有都存在
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        bool All(Expression<Func<T, bool>> whereLambda);

        T FirstOrDefault(Expression<Func<T, bool>> whereLambda);

        T FirstOrDefaultNoTracking(Expression<Func<T, bool>> whereLambda);
    }
}