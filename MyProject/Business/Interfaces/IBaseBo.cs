using Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Business.Interfaces
{
    public interface IBaseBo<TModel, TEntity>
        where TModel : class
        where TEntity : class
    {
        TModel GetById(object id);
        TModel GetFirst(Func<TEntity, bool> filter);
        TModel GetFirstIgnoreNull(Func<TEntity, bool> filter);
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> Get(Func<TEntity, bool> filter);
        TModel Insert(TModel item);
        TModel Insert(TModel item, IUnitOfWork unitOfWork);
        TModel Update(TModel item, object id);
        TModel Update(TModel item, object id, IUnitOfWork unitOfWork);
        bool Delete(object id);
        bool Delete(object id, IUnitOfWork unitOfWork);
        IUnitOfWork NewDbContext();
    }
}
