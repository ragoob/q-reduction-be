using QReduction.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QReduction.Core.Service.Generic
{
    public interface IService<TEntity> where TEntity : class
    {
        #region Insert

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Update
        void Edit(TEntity entity);
        void EditRange(IEnumerable<TEntity> entities);

        Task EditAsync(TEntity entity);
        Task EditRangeAsync(IEnumerable<TEntity> entities);

        #endregion
        
        #region Delete
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Select
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);

        IEnumerable<TEntity> Get(BaseSearchModel searchModel);
        IEnumerable<TEntity> Get(BaseSearchModel searchModel, params string[] includingTables);

        Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel);
        Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel, params string[] includingTables);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetAll(params string[] includingTables);
        Task<IEnumerable<TEntity>> GetAllAsync(params string[] includingTables);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);


        IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables);
        Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables);


        bool Any(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        bool Any(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables);

        int Count();
        Task<int> CountAsync();

        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        T Max<T>(Expression<Func<TEntity, T>> predicate);
        T Max<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition);

        Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate);
        Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
