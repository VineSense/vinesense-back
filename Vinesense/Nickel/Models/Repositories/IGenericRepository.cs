﻿using System;
using System.Linq;
using System.Linq.Expressions;
namespace Nickel.Models
{
    public interface IGenericRepository<TEntity>
     where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
