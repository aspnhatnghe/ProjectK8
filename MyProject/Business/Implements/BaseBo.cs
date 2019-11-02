using Business.Interfaces;
using System;
using System.Collections.Generic;
using Repositories;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Entities;

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
        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TModel> Get(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            var models = new List<TModel>();

            using (var unitOfWork = NewDbContext())
            {
                var repositories = unitOfWork.Respository<TEntity>();

                var entities = repositories.GetAll();
                models = _mapper.Map<List<TModel>>(entities);
            }

            return models;
        }

        public TModel GetById(object id)
        {
            throw new NotImplementedException();
        }

        public TModel GetFirst(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        public TModel GetFirstIgnoreNull(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        public TModel Insert(TModel item)
        {
            throw new NotImplementedException();
        }

        public TModel Insert(TModel item, IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
        }

        public IUnitOfWork NewDbContext()
        {
            var context = _serviceProvider.GetRequiredService<MyDbContext>();

            return new UnitOfWork(context);
        }

        public TModel Update(TModel item, object id)
        {
            throw new NotImplementedException();
        }

        public TModel Update(TModel item, object id, IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
        }
    }
}
