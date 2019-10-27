using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity Find(object id);

        TEntity GetById(object id);

        TEntity Insert(TEntity entityToInsert);

        List<TEntity> Insert(List<TEntity> entitiesToInsert);

        TEntity Update(TEntity entityToUpdate);

        bool Delete(object id);

        bool Delete(TEntity entityToDelete);

        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);

        IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where);

        TEntity GetFirstOrDefault(Func<TEntity, Boolean> where);

        bool Delete(Func<TEntity, Boolean> where);

        IQueryable<TEntity> GetEntitiesWithInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetEntitiesWithInclude(params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> GetEntities();

        bool Exists(object primaryKey);

        TEntity GetSingle(Func<TEntity, bool> predicate);

        TEntity GetFirst(Func<TEntity, bool> predicate);

        int ExecuteSqlCommand(string query, params object[] @params);

        List<T> FromSql<T>(string query, params object[] @params) where T : class;
    }
}