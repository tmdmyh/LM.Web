

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.BulkInsert.Extensions;
using EntityFramework.Extensions;




namespace LM.Core.Data
{
    /// <summary>
    /// EntityFramework的仓储实现
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _db;
        private  DbSet<T> _dbSet;
        private  IQueryable<T> _table;
        private  IQueryable<T> _tableNoTracking;


        public Repository(DbContext db)
        {
            this._db = db;
            this._dbSet = db.Set<T>();
            this._tableNoTracking = this._dbSet.AsNoTracking();
            this._table = this._dbSet; 
        }



        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="Table"/>数据集
        /// </summary>
        public IQueryable<T> TableNoTracking
        {
            get { return _tableNoTracking; }
            set { _tableNoTracking = value; }
        }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="TableNoTracking"/>数据集
        /// </summary>
        public IQueryable<T> Table
        {
            get { return _table; }
            set { this._table = value; }
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public void Add(T entity)
        {
            this._dbSet.Add(entity);
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public void Add(IEnumerable<T> entities)
        {
            entities = entities as T[] ?? entities.ToArray();
            this._dbSet.AddRange(entities);
        }
       


    
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public void Delete(T entity)
        {
            this._dbSet.Remove(entity);
        }

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public void Delete(object key)
        {
            T entity = this._dbSet.Find(key);
            Delete(entity);
        }

        /// <summary>
        /// 删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            T[] entities = this._dbSet.Where(predicate).ToArray();
             Delete(entities);
        }

        /// <summary>
        /// 批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public void Delete(IEnumerable<T> entities)
        {
            entities = entities as T[] ?? entities.ToArray();
            this._dbSet.RemoveRange(entities);
        }

      
        
        /// <summary>
        /// 直接删除所有符合特定条件的实体，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public void DeleteDirect(Expression<Func<T, bool>> predicate)
        {
           this._dbSet.Where(predicate).Delete();
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                if (_db.Entry<T>(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);

                    _db.Entry<T>(entity).State = EntityState.Modified;
                }
            }
        }
        public void Update(T entity)
        {
            if (_db.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);

                _db.Entry<T>(entity).State = EntityState.Modified;
            }
        }
      

        /// <summary>
        /// 直接更新指定条件的数据，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public int UpdateDirect(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatExpression)
        {
            return _dbSet.Where(predicate).Update(updatExpression);
        }

        public bool Any(Expression<Func<T, bool>> whereLambda)
        {
            return _dbSet.Any(whereLambda);
        }

        /// <summary>
        /// 查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public T GetByKey(object key)
        {
            return _dbSet.Find(key);
        }
        
        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        public IQueryable<T> GetInclude<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return _table.Include(path);
        }

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集不追踪
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        public IQueryable<T> GetIncludeAsNoTracking<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return _tableNoTracking.Include(path);
        }
        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        public IQueryable<T> GetIncludes(params string[] paths)
        {
            IQueryable<T> source = _table;
            foreach (string path in paths)
            {
                source = source.Include(path);
            }
            return source;
        }
        public IQueryable<T> GetIncludesNoTracking(params string[] paths)
        {
            IQueryable<T> source = _tableNoTracking;
            foreach (string path in paths)
            {
                source = source.Include(path);
            }
            return source;
        }


        /// <summary>
        /// 高速批量插入数据
        /// </summary>
        /// <param name="entities"></param>
        public void BulkInsert(IEnumerable<T> entities)
        {
            
            _db.BulkInsert(entities);

        }
        /// <summary>
        /// 判断所有是否都存在
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns>是,否</returns>

        public bool All(Expression<Func<T, bool>> whereLambda)
        {
            return _tableNoTracking.All(whereLambda);
        }
        /// <summary>
        /// 根据条件查找单条数据，带追踪
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> whereLambda)
        {
            return _table.FirstOrDefault(whereLambda);
        }
        /// <summary>
        ///  根据条件查找单条数据，不带追踪
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T FirstOrDefaultNoTracking(Expression<Func<T, bool>> whereLambda)
        {
            return _tableNoTracking.FirstOrDefault(whereLambda);
        }
    }
}