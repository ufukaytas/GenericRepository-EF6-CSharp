using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationName.Data.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync();
        TEntity Find(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> Query(); 
        int Add(TEntity entity);
        Task<int> AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity); 
    } 
}
