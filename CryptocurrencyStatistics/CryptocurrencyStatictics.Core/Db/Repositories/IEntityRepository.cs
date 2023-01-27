using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using CryptocurrencyStatictics.Core.Db.DbModels;

namespace CryptocurrencyStatictics.Core.Db.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : BaseEntity, new()
    {
        void Create(TEntity entity);
        void Create(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        TEntity FirstOrDefault(Func<TEntity, bool> where);
        TEntity LastOrDefault(Func<TEntity, bool> where);
    }
}
