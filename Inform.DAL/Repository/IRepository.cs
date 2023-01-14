using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inform.DAL.Repository
{
   public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity);
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
    }
}
