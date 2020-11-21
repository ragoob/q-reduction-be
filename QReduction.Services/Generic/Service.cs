using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QReduction.Core.Extensions;
using QReduction.Core.Models;
using QReduction.Core.Repository.Generic;
using QReduction.Core.Service.Generic;
using QReduction.Infrastructure.UnitOfWorks;

namespace QReduction.Services.Generic
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IQReductionUnitOfWork _unitOfWork;

        #region Ctor
        public Service(IQReductionUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        #endregion

        #region IService
        public virtual void Add(TEntity entity)
        {
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
        }
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
            _unitOfWork.SaveChanges();
        }
        public virtual void Edit(TEntity entity)
        {
            _repository.Edit(entity);
            _unitOfWork.SaveChanges();
        }
        public virtual void EditRange(IEnumerable<TEntity> entities)
        {
            entities.ForEach(e => _repository.Edit(e));
            _unitOfWork.SaveChanges();
        }


        public virtual void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            _unitOfWork.SaveChanges();
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
            _unitOfWork.SaveChanges();
        }

        public virtual TEntity GetById(object id)
        {
            return _repository.GetById(id);
        }
        public virtual IEnumerable<TEntity> Get(BaseSearchModel searchModel)
        {
            return _repository.Get(searchModel);
        }
        public IEnumerable<TEntity> Get(BaseSearchModel searchModel, params string[] includingTables)
        {
            return _repository.Get(searchModel, includingTables);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }
        public virtual IEnumerable<TEntity> GetAll(params string[] includingTables)
        {
            return _repository.GetAll(includingTables);
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate);
        }
        public virtual IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(searchModel, predicate);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return _repository.Find(predicate, includingTables);
        }
        public virtual IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return _repository.Find(searchModel, predicate, includingTables);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Any(predicate);
        }
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return _repository.Any(predicate, includingTables);
        }

        public virtual Task AddAsync(TEntity entity)
        {
            _repository.Add(entity);
            return _unitOfWork.SaveChangesAsync();
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
            return _unitOfWork.SaveChangesAsync();
        }

        public virtual Task EditAsync(TEntity entity)
        {
            _repository.Edit(entity);
            return _unitOfWork.SaveChangesAsync();
        }
        public Task EditRangeAsync(IEnumerable<TEntity> entities)
        {
            entities.ForEach(e => _repository.Edit(e));
            return _unitOfWork.SaveChangesAsync();
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);
            return _unitOfWork.SaveChangesAsync();
        }

        public virtual Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
            return _unitOfWork.SaveChangesAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return _repository.GetByIdAsync(id);
        }

        public virtual Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel)
        {
            return _repository.GetAsync(searchModel);
        }

        public Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel, params string[] includingTables)
        {
            return _repository.GetAsync(searchModel, includingTables);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(params string[] includingTables)
        {
            return _repository.GetAllAsync(includingTables);
        }

        public virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FindAsync(predicate);
        }

        public virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return _repository.FindAsync(predicate, includingTables);
        }

        public virtual Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FindAsync(searchModel, predicate);
        }

        public virtual Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return _repository.FindAsync(searchModel, predicate, includingTables);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.AnyAsync(predicate);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return _repository.AnyAsync(predicate, includingTables);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.CountAsync(predicate);
        }

        public T Max<T>(Expression<Func<TEntity, T>> predicate)
        {
            return _repository.Max<T>(predicate);
        }

        public T Max<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition)
        {
            return _repository.Max<T>(predicate, predicateCondition);
        }

        public Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate)
        {
            return _repository.MaxAsync<T>(predicate);
        }

        public Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition)
        {
            return _repository.MaxAsync<T>(predicate, predicateCondition);
        }

        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FindOneAsync(predicate);
        }

        #endregion
    }
}
