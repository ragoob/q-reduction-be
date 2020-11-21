using Microsoft.EntityFrameworkCore;
using QReduction.Core.Models;
using QReduction.Core.Repository.Generic;
using QReduction.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QReduction.Infrastructure.Repositories.Generic
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        #region Fields
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<TEntity> _entitiesSet;
        #endregion

        #region ctor
        public Repository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _entitiesSet = _databaseContext.Set<TEntity>();
        }
        #endregion

        #region Repository

        #region Add
        public void Add(TEntity entity)
        {
            _entitiesSet.Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entitiesSet.AddRange(entities);
        }
        #endregion

        #region Edit
        public void Edit(TEntity entity)
        {
            _entitiesSet.Update(entity);
        }
        #endregion

        #region select
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitiesSet.Any(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

            if (includingTables != null)
                for (int i = 0; i < includingTables.Length; i++)
                    entitiesList = entitiesList.Include(includingTables[i]);

            return entitiesList.Any(predicate);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitiesSet.AnyAsync(predicate);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

            if (includingTables != null)
                for (int i = 0; i < includingTables.Length; i++)
                    entitiesList = entitiesList.Include(includingTables[i]);

            return entitiesList.AnyAsync(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitiesSet.Where(predicate).AsNoTracking().AsEnumerable<TEntity>();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            IQueryable<TEntity> entitiesList = _entitiesSet.AsNoTracking().AsQueryable();

            if (includingTables != null)
                for (int i = 0; i < includingTables.Length; i++)
                    entitiesList = entitiesList.Include(includingTables[i]);

            return entitiesList
                .Where(predicate)
                .AsEnumerable<TEntity>();
        }

        public IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate)
        {
            int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;

            var entitiesList = _entitiesSet
                .Where(predicate)
                .OrderBy(searchModel.SortBy)
                .Skip(start)
                .Take(searchModel.PageSize)
                .AsEnumerable<TEntity>();


            searchModel.TotalRowsCount = _entitiesSet.Count(predicate);
            searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

            return entitiesList;
        }

        public IEnumerable<TEntity> Find(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;

            IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

            if (includingTables != null)
                for (int i = 0; i < includingTables.Length; i++)
                    entitiesList = entitiesList.Include(includingTables[i]);

            entitiesList = entitiesList
                .Where(predicate)
                .OrderBy(searchModel.SortBy)
                .Skip(start)
                .Take(searchModel.PageSize);

            searchModel.TotalRowsCount = _entitiesSet.Count(predicate);
            searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

            return entitiesList.AsEnumerable<TEntity>();
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return
                Task.Run(() =>
                        _entitiesSet
                            .Where(predicate)
                            .AsNoTracking()
                            .AsEnumerable<TEntity>()
                    );
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return
                Task.Run(() =>
                {

                    IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

                    if (includingTables != null)
                        for (int i = 0; i < includingTables.Length; i++)
                            entitiesList = entitiesList.Include(includingTables[i]);

                    return entitiesList
                            .AsNoTracking()
                            .Where(predicate)
                            .AsEnumerable<TEntity>();
                });
        }

        public Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate)
        {
            return
                 Task.Run(() =>
                         {
                             IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

                             int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;

                             entitiesList = entitiesList
                                 .Where(predicate)
                                 .OrderBy(searchModel.SortBy)
                                 .Skip(start)
                                 .Take(searchModel.PageSize);

                             searchModel.TotalRowsCount = _entitiesSet.Count(predicate);
                             searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

                             return entitiesList.AsEnumerable<TEntity>();
                         }
                     );
        }

        public Task<IEnumerable<TEntity>> FindAsync(BaseSearchModel searchModel, Expression<Func<TEntity, bool>> predicate, params string[] includingTables)
        {
            return
            Task.Run(() =>
            {
                IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

                if (includingTables != null)
                    for (int i = 0; i < includingTables.Length; i++)
                        entitiesList = entitiesList.Include(includingTables[i]);

                int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;

                entitiesList = entitiesList
                    .Where(predicate)
                    .OrderBy(searchModel.SortBy)
                    .Skip(start)
                    .Take(searchModel.PageSize);

                searchModel.TotalRowsCount = _entitiesSet.Count(predicate);
                searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

                return entitiesList.AsEnumerable<TEntity>();
            }
                );
        }

        public IEnumerable<TEntity> Get(BaseSearchModel searchModel)
        {
            int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;

            var entitiesList = _entitiesSet
                .OrderBy(searchModel.SortBy)
                .Skip(start)
                .Take(searchModel.PageSize)
                .ToList();

            searchModel.TotalRowsCount = _entitiesSet.Count();
            searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

            return entitiesList;
        }

        public IEnumerable<TEntity> Get(BaseSearchModel searchModel, params string[] includingTables)
        {
            int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;

            IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

            if (includingTables != null)
                for (int i = 0; i < includingTables.Length; i++)
                    entitiesList = entitiesList.Include(includingTables[i]);


            entitiesList = entitiesList
                .OrderBy(searchModel.SortBy)
                .Skip(start)
                .Take(searchModel.PageSize);

            searchModel.TotalRowsCount = _entitiesSet.Count();
            searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

            return entitiesList.AsEnumerable<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entitiesSet.AsEnumerable<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(params string[] includingTables)
        {
            IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

            if (includingTables != null)
                for (int i = 0; i < includingTables.Length; i++)
                    entitiesList = entitiesList.Include(includingTables[i]);

            return entitiesList.AsEnumerable<TEntity>();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.Run(() => _entitiesSet.AsEnumerable<TEntity>());
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(params string[] includingTables)
        {
            return Task.Run(() =>
            {
                IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

                if (includingTables != null)
                    for (int i = 0; i < includingTables.Length; i++)
                        entitiesList = entitiesList.Include(includingTables[i]);

                return entitiesList.AsEnumerable<TEntity>();
            });
        }

        public Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel)
        {
            return Task.Run(() =>
            {
                int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;

                var entitiesList = _entitiesSet
                    .OrderBy(searchModel.SortBy)
                    .Skip(start)
                    .Take(searchModel.PageSize)
                    .AsEnumerable<TEntity>();

                searchModel.TotalRowsCount = _entitiesSet.Count();
                searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);


                return entitiesList;
            });
        }

        public Task<IEnumerable<TEntity>> GetAsync(BaseSearchModel searchModel, params string[] includingTables)
        {
            return Task.Run(() =>
            {
                int start = (searchModel.CurrentPage - 1) * searchModel.PageSize;
                IQueryable<TEntity> entitiesList = _entitiesSet.AsQueryable();

                if (includingTables != null)
                    for (int i = 0; i < includingTables.Length; i++)
                        entitiesList = entitiesList.Include(includingTables[i]);

                entitiesList = entitiesList
                    .OrderBy(searchModel.SortBy)
                    .Skip(start)
                    .Take(searchModel.PageSize);

                searchModel.TotalRowsCount = _entitiesSet.Count();
                searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

                return entitiesList.AsEnumerable<TEntity>();
            });
        }

        public TEntity GetById(object id)
        {
            return _entitiesSet.Find(id);
        }

        public Task<TEntity> GetByIdAsync(object id)
        {
            return _entitiesSet.FindAsync(id);
        }

        public int Count()
        {
            return _entitiesSet.Count();
        }

        public Task<int> CountAsync()
        {
            return _entitiesSet.CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitiesSet.Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitiesSet.CountAsync(predicate);
        }

        public T Max<T>(Expression<Func<TEntity, T>> predicate)
        {
            if (!_entitiesSet.Any())
                return default(T);

            return _entitiesSet.Max(predicate);
        }

        public T Max<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition)
        {
            if (!_entitiesSet.Any(predicateCondition))
                return default(T);

            return _entitiesSet.Where(predicateCondition).Max(predicate);
        }

        public async Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate, Expression<Func<TEntity, bool>> predicateCondition)
        {
            if (!await _entitiesSet.AnyAsync(predicateCondition))
                return default(T);

            return await _entitiesSet.Where(predicateCondition).MaxAsync(predicate);
        }
        public async Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> predicate)
        {
            if (!await _entitiesSet.AnyAsync())
                return default(T);

            return await _entitiesSet.MaxAsync(predicate);
        }

        #endregion

        #region Remove
        public void Remove(TEntity entity)
        {
            _entitiesSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entitiesSet.RemoveRange(entities);
        }

        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entitiesSet.FirstOrDefaultAsync(predicate);
        }

        #endregion


        #endregion

    }
}
