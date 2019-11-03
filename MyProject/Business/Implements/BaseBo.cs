using Business.Interfaces;
using System;
using System.Collections.Generic;
using Repositories;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Entities;
using System.Linq.Expressions;

namespace Business.Implements
{
    public class BaseBo<TModel, TEntity> : IBaseBo<TModel, TEntity>
        where TModel : class
        where TEntity : class
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        public BaseBo(IMapper mapper, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        #region Delete
        public bool Delete(object id)
        {
            using (var unitOfWork = NewDbContext())
            {
                return Delete(id, unitOfWork);
            }
        }

        public bool Delete(object id, IUnitOfWork unitOfWork)
        {
            try
            {
                // Get repository.
                var repository = unitOfWork.Repository<TEntity>();

                // Delete model.
                repository.Delete(id);

                // Save change to datacontext
                return unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteByEntity(TEntity entity)
        {
            using (var unitOfWork = NewDbContext())
            {
                return DeleteByEntity(entity, unitOfWork);
            }
        }

        public bool DeleteByEntity(TEntity entity, IUnitOfWork unitOfWork)
        {
            try
            {
                // Get repository.
                var repository = unitOfWork.Repository<TEntity>();

                // Delete model.
                repository.Delete(entity);

                // Save change to datacontext
                unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteEntity(TModel entity)
        {
            using (var unitOfWork = NewDbContext())
            {
                return DeleteEntity(entity, unitOfWork);
            }
        }

        public bool DeleteEntity(TModel item, IUnitOfWork unitOfWork)
        {
            try
            {
                // Get repository.
                var repository = unitOfWork.Repository<TEntity>();

                var entity = _mapper.Map<TEntity>(item);

                // Delete model.
                repository.Delete(entity);

                // Save change to datacontext
                unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public int ExecuteSqlCommand(string query, params object[] @params)
        {
            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();
                return repository.ExecuteSqlCommand(query, @params);
            }
        }

        public int ExecuteSqlCommand(IUnitOfWork unitOfWork, string query, params object[] @params)
        {
            var repository = unitOfWork.Repository<TEntity>();
            return repository.ExecuteSqlCommand(query, @params);
        }

        public List<T> ExecuteStoreProcedure<T>(string query, params object[] @params) where T : class
        {
            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();
                return repository.FromSql<T>(query, @params);
            }
        }

        public bool Exists(object id)
        {
            bool exist;

            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();
                exist = repository.Exists(id);
            }

            return exist;
        }

        public IEnumerable<TModel> Get(Func<TEntity, bool> filter)
        {
            try
            {
                var models = new List<TModel>();

                using (var unitOfWork = NewDbContext())
                {
                    var repositories = unitOfWork.Repository<TEntity>();

                    var entities = repositories.GetMany(filter);

                    models = _mapper.Map<List<TModel>>(entities);
                }

                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            try
            {
                var models = new List<TModel>();

                using (var unitOfWork = NewDbContext())
                {
                    var repositories = unitOfWork.Repository<TEntity>();

                    var entities = repositories.GetAll();
                    models = _mapper.Map<List<TModel>>(entities);
                }

                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModel GetById(object id)
        {
            TModel result = null;
            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();
                var item = repository.GetById(id);

                result = _mapper.Map<TModel>(item);
            }

            return result;
        }

        public IQueryable<TEntity> GetEntities(IUnitOfWork unitOfWork)
        {
            var repository = unitOfWork.Repository<TEntity>();
            return repository.GetEntities();
        }

        public IQueryable<TEntity> GetEntities(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> filter)
        {
            var repository = unitOfWork.Repository<TEntity>();
            return repository.GetEntities(filter);
        }

        public IQueryable<TEntity> GetEntitiesWithInclude(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var repository = unitOfWork.Repository<TEntity>();
            return repository.GetEntitiesWithInclude(filter, includes);
        }
        public IQueryable<TEntity> GetEntitiesWithInclude(IUnitOfWork unitOfWork, params Expression<Func<TEntity, object>>[] includes)
        {
            var repository = unitOfWork.Repository<TEntity>();
            return repository.GetEntitiesWithInclude(includes);
        }
        public IQueryable<TEntity> GetEntitiesWithInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();
                return repository.GetEntitiesWithInclude(filter, includes);
            }
        }

        public TModel GetFirst(Func<TEntity, bool> filter)
        {
            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();

                var item = repository.GetFirst(filter);

                return _mapper.Map<TModel>(item);
            }
        }

        public TModel GetFirstIgnoreNull(Func<TEntity, bool> filter)
        {
            try
            {
                TModel models = null;

                using (var unitOfWork = NewDbContext())
                {
                    var repository = unitOfWork.Repository<TEntity>();

                    var entities = repository.GetMany(filter);
                    if (entities.Any())
                    {
                        models = _mapper.Map<TModel>(entities.First());
                    }

                }

                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModel GetFirstWithInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();
                var item = repository.GetEntitiesWithInclude(filter, includes).First();
                return _mapper.Map<TModel>(item);
            }
        }
        public TModel GetFirstWithIncludeIgnoreNull(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var unitOfWork = NewDbContext())
            {
                var repository = unitOfWork.Repository<TEntity>();
                var item = repository.GetEntitiesWithInclude(filter, includes);
                if (item.Any())
                {
                    return _mapper.Map<TModel>(item.First());
                }
            }
            return null;
        }

        public IEnumerable<TModel> GetIgnoreNull(Func<TEntity, bool> filter)
        {
            try
            {
                var models = new List<TModel>();

                using (var unitOfWork = NewDbContext())
                {
                    var repository = unitOfWork.Repository<TEntity>();

                    var entities = repository.GetMany(filter);
                    if (entities.Any())
                    {
                        models = _mapper.Map<List<TModel>>(entities);
                    }
                }

                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TModel> GetTop(Func<TEntity, bool> filter, int count, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {
                var models = new List<TModel>();

                // Get entity from data context.
                using (var unitOfWork = NewDbContext())
                {
                    // Get repository.
                    var repository = unitOfWork.Repository<TEntity>();

                    // Get all entity in datacontext.
                    var entities = orderBy(repository.GetManyQueryable(filter)).Take(count);

                    models = _mapper.Map<List<TModel>>(entities);
                    // Map model list to models list.

                }

                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModel Insert(TModel item)
        {
            using (var unitOfWork = NewDbContext())
            {
                return Insert(item, unitOfWork);
            }
        }

        public TModel Insert(TModel item, IUnitOfWork unitOfWork)
        {
            try
            {
                var repositories = unitOfWork.Repository<TEntity>();

                var entity = _mapper.Map<TEntity>(item);

                var entityResult = repositories.Insert(entity);

                bool result = unitOfWork.Save();
                if (result)
                {
                    return _mapper.Map<TModel>(entityResult);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity InsertEntity(TEntity item)
        {
            using (var unitOfWork = NewDbContext())
            {
                return InsertEntity(item, unitOfWork);

            }
        }

        public TEntity InsertEntity(TEntity item, IUnitOfWork unitOfWork)
        {
            try
            {
                var repository = unitOfWork.Repository<TEntity>();

                var entityResult = repository.Insert(item);

                unitOfWork.Save();

                return entityResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IUnitOfWork NewDbContext()
        {
            var context = _serviceProvider.GetRequiredService<MyDbContext>();

            return new UnitOfWork(context);
        }

        public TModel Update(TModel item, object id)
        {
            using (var unitOfWork = NewDbContext())
            {
                return Update(item, id, unitOfWork);
            }
        }

        public TModel Update(TModel item, object id, IUnitOfWork unitOfWork)
        {
            try
            {
                // Get repository.
                var repository = unitOfWork.Repository<TEntity>();

                // Get Entity
                var entity = repository.Find(id);
                if (entity != null)
                {
                    // Mapping to TEntity
                    entity = _mapper.Map(item, entity);
                }

                // Update TEntity
                var entityResult = repository.Update(entity);

                // Save change to datacontext.
                unitOfWork.Save();

                // Return TModel
                return _mapper.Map<TModel>(entityResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModel UpdateEntity(TEntity item, object id)
        {
            using (var unitOfWork = NewDbContext())
            {
                return UpdateEntity(item, id, unitOfWork);

            }
        }

        public TModel UpdateEntity(TEntity item, object id, IUnitOfWork unitOfWork)
        {
            try
            {
                // Get repository.
                var repository = unitOfWork.Repository<TEntity>();

                // Get Entity
                var entity = repository.Find(id);

                if (entity != null)
                {
                    // Mapping to TEntity
                    entity = _mapper.Map(item, entity);
                }

                // Update TEntity
                var entityResult = repository.Update(entity);

                // Save change to datacontext.
                unitOfWork.Save();

                // Return TModel
                return _mapper.Map<TModel>(entityResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
