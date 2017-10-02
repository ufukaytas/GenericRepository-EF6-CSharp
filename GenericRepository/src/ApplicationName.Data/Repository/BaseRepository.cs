using ApplicationName.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ApplicationName.EF.Context;
using ApplicationName.Data.Entitites;

namespace ApplicationName.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {

        #region Field
        protected EntitiesContext _dbContext;
        #endregion

        #region Ctor
        /// <summary>
        /// The contructor requires an open DataContext to work with
        /// </summary>
        /// <param name="context">An open DataContext</param>
        public BaseRepository(EntitiesContext context)
        {
            _dbContext = context;
        }
        #endregion
         
        #region CRUD

        public TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public IList<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public int Add(TEntity entity)
        { 
            var addedItem = _dbContext.Set<TEntity>().Add(entity);
            Commit();

            var propertyInfo = addedItem.GetType().GetProperty("Id");
            if (propertyInfo != null)
            {
                var idProperty = propertyInfo.GetValue(entity, null);
                return (int)idProperty;
            }

            return -1;
        }

        public void Update(TEntity entity)
        { 
            _dbContext.Set<TEntity>().AddOrUpdate(entity);
            Commit();
        }

        public void Delete(TEntity entity)
        { 
            _dbContext.Set<TEntity>().Remove(entity);
            Commit();
        }
         
        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return _dbContext.Set<TEntity>().SingleOrDefault(match);
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return _dbContext.Set<TEntity>().Where(match).ToList();
        }

        public bool Any(Expression<Func<TEntity, bool>> match)
        {
            return _dbContext.Set<TEntity>().Any(match);
        }
         
        public IQueryable<TEntity> Query()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Async

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> AddAsync(TEntity entity)
        { 
            var addedItem = _dbContext.Set<TEntity>().Add(entity);
            await CommitAsync();

            var propertyInfo = addedItem.GetType().GetProperty("Id");
            if (propertyInfo != null)
            {
                var idProperty = propertyInfo.GetValue(entity, null);
                return (int)idProperty;
            }

            return -1;
        }

        public async Task UpdateAsync(TEntity entity)
        { 
            _dbContext.Set<TEntity>().AddOrUpdate(entity);
            await CommitAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        { 
            _dbContext.Set<TEntity>().Remove(entity);
            await CommitAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _dbContext.Set<TEntity>().Where(match).ToListAsync();
        }

        #endregion

        #region Commit

        private void Commit()
        {
            _dbContext.SaveChanges();
        }

        private async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
  

        public Task<IQueryable<TEntity>> QueryAsync()
        {
            throw new NotImplementedException();
        }



        #endregion

    }

}
