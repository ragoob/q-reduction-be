using QReduction.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QReduction.Core.Repository.Generic
{
    public interface IRepository<TEntity> where TEntity : class
    {

        #region Insert
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        #endregion

        #region Update
        void Edit(TEntity entity);
        #endregion

        #region Delete
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        #endregion

        #region Select
        TEntity GetById(object id);

        IEnumerable<TEntity> Get(BaseSearchModel searchModel);
        IEnumerable<TEntity> Get(BaseSearchModel searchModel, params string[] includingTables);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(params string[] includingTables);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);

        IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables);

        bool Any(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);

        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);

        T Max<T>(Expression<Func<TEntity, T>> predicate);
        T Max<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition);
        #endregion

        #region Select Async
        Task<TEntity> GetByIdAsync(object id);

        Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(params string[] includingTables);
        Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel, params string[] includingTables);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);

        Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate);
        Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition);

           Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

    }

}
