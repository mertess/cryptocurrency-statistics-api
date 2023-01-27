using CryptocurrencyStatictics.Core.Db.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CryptocurrencyStatictics.Core.Db.Repositories
{
    public abstract class BaseEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseEntityRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Create(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Create(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Delete(entity);
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> where)
        {
            // По какой то причине на постгрес провайдере не позволяет составные условия выполнять на стороне бд, поэтому добавил ToList()
            // что явно скажется на производительности
            return _dbSet.ToList().FirstOrDefault(where);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public TEntity LastOrDefault(Func<TEntity, bool> where)
        {
            return _dbSet.ToList().LastOrDefault(where);
        }
    }
}
