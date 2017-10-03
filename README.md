# GenericRepository-EF6-CSharp
Generic Repository Pattern with Entity Framework 6 for .Net 4.5


```csharp
public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {

        #region Field
        public Expression<Func<TEntity, bool>> Filter { get; set; }

        protected EntitiesContext DbContext;

        private bool _disposed = false;
        #endregion

        #region Ctor
        /// <summary>
        /// The contructor requires an open DataContext to work with
        /// </summary>
        /// <param name="context">An open DataContext</param>
        public BaseRepository(EntitiesContext context)
        {
            DbContext = context;
        }
        #endregion
         
        #region CRUD

        public TEntity Get(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public IList<TEntity> GetAll()
        {
            return  Filter == null ? DbContext.Set<TEntity>().ToList() : DbContext.Set<TEntity>().Where(Filter).ToList();
        }

        public int Add(TEntity entity)
        {
            var properyInfo = entity.GetType().GetProperty("CreatedDate");
            if (properyInfo != null)
            {
                properyInfo.SetValue(entity, DateTime.Now);
            }
             
            var addedItem = DbContext.Set<TEntity>().Add(entity);
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
            var properyInfo = entity.GetType().GetProperty("UpdatedDate");
            if (properyInfo != null)
            {
                properyInfo.SetValue(entity, DateTime.Now);
            }

            DbContext.Set<TEntity>().AddOrUpdate(entity);
            Commit();
        }

        public void Delete(TEntity entity)
        { 
            DbContext.Set<TEntity>().Remove(entity);
            Commit();
        }
         
        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return DbContext.Set<TEntity>().SingleOrDefault(match);
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return DbContext.Set<TEntity>().Where(match).ToList();
        }

        public bool Any(Expression<Func<TEntity, bool>> match)
        {
            return DbContext.Set<TEntity>().Any(match);
        }
         
        public IQueryable<TEntity> Query()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        #endregion

        #region Async

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            var properyInfo = entity.GetType().GetProperty("CreatedDate");
            if (properyInfo != null)
            {
                properyInfo.SetValue(entity, DateTime.Now);
            }

            var addedItem = DbContext.Set<TEntity>().Add(entity);
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
            var properyInfo = entity.GetType().GetProperty("UpdatedDate");
            if (properyInfo != null)
            {
                properyInfo.SetValue(entity, DateTime.Now);
            }

            DbContext.Set<TEntity>().AddOrUpdate(entity);
            await CommitAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        { 
            DbContext.Set<TEntity>().Remove(entity);
            await CommitAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbContext.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbContext.Set<TEntity>().Where(match).ToListAsync();
        }

        #endregion

        #region Commit

        private void Commit()
        {
            DbContext.SaveChanges();
        }

        private async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        #endregion


        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (DbContext != null)
                    {
                        DbContext.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
 ```

