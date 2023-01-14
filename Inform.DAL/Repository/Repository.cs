using Inform.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inform.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal InformDBContext _dbContext;
        internal DbSet<TEntity> _entities;

        public Repository(InformDBContext informDBContext)
        {
            _dbContext = informDBContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public bool Add(TEntity entity)
        {
            _entities.Add(entity);

            return Save();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);

            return await SaveAsync();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.FirstOrDefault(filter);
        }
        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }
        public IQueryable<TEntity> GetAll()
        {
            return _entities.AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.Where(filter).AsQueryable<TEntity>();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_entities.AsQueryable<TEntity>());
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Task.FromResult(_entities.Where(filter).AsQueryable<TEntity>());
        }

        public bool Update(TEntity entity)
        {
            UpdateEntityState(entity, EntityState.Modified);
            return Save();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            UpdateEntityState(entity, EntityState.Modified);
            return await SaveAsync();
        }

        public void UpdateEntityState(TEntity entity, EntityState entityState)
        {
            var dbEntityEntry = GetDbEntityEntry(entity);
            dbEntityEntry.State = entityState;
        }

        public EntityEntry GetDbEntityEntry(TEntity entity)
        {
            var dbEntityEntry = _dbContext.Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        public async Task<bool> SaveAsync()
        {

           return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;

        }

    }
}
