﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private DbSet<TEntity> entities;
        string errorMessage = string.Empty;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }

        public bool Delete(object id)
        {
            var entityToDelete = entities.Find(id);
            if (entityToDelete != null)
            {
                return Delete(entityToDelete);
            }
            return false;
        }

        public bool Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException("entityToDelete");
            }
            if (entities.Remove(entityToDelete) != null)
            {
                return true;
            }

            return false;
        }

        public bool Delete(Func<TEntity, bool> where)
        {
            bool isDeleted = false;

            var objects = entities.Where(where).AsQueryable();
            foreach (var obj in objects)
            {
                if (entities.Remove(obj) != null)
                {
                    isDeleted = true;
                }
            }

            return isDeleted;
        }

        public int ExecuteSqlCommand(string query, params object[] @params)
        {   
            return context.Database.ExecuteSqlCommand(query, @params);
        }

        public bool Exists(object primaryKey)
        {
            return entities.Find(primaryKey) != null;
        }

        public TEntity Find(object id)
        {
            return entities.Find(id);
        }

        public List<T> FromSql<T>(string query, params object[] @params) where T : class
        {
            var res = ExecSQL<T>(query, @params);
            return res;
        }
        private List<T> ExecSQL<T>(string query, params object[] @params)
        {
            using (context)
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    //Parameters
                    if (@params != null && @params.Length > 0)
                    {
                        Regex regParameters = new Regex(@"@\w+", RegexOptions.Compiled);

                        MatchCollection cmdParams = regParameters.Matches(command.CommandText);
                        List<String> param = new List<String>();
                        foreach (var el in cmdParams)
                        {
                            if (!param.Contains(el.ToString()))
                            {
                                param.Add(el.ToString());
                            }
                        }
                        Int32 i = 0;
                        IDbDataParameter dp;
                        foreach (String el in param)
                        {
                            dp = command.CreateParameter();
                            dp.ParameterName = el;
                            dp.Value = @params[i++];
                            command.Parameters.Add(dp);
                        }
                    }

                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        List<T> list = new List<T>();
                        T obj = default(T);
                        var oType = typeof(T);
                        while (result.Read())
                        {
                            if (oType == typeof(string))
                            {
                                if (result.VisibleFieldCount > 0)
                                {
                                    obj = (T)Convert.ChangeType(result[0], typeof(T));
                                }
                            }
                            else
                            {
                                obj = Activator.CreateInstance<T>();

                                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                                {
                                    for (int i = 0; i < result.VisibleFieldCount; i++)
                                    {
                                        if (result.GetName(i).Equals(prop.Name))
                                        {
                                            if (!object.Equals(result[prop.Name], DBNull.Value))
                                            {
                                                if (prop.PropertyType.ToString().Equals("System.Nullable`1[System.Guid]"))
                                                {
                                                    var valueTmp = result[prop.Name];
                                                    if (valueTmp != null)
                                                    {
                                                        var value = Guid.Parse(valueTmp.ToString());
                                                        prop.SetValue(obj, value, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(obj, Guid.Empty, null);
                                                    }
                                                }
                                                else if (prop.PropertyType.ToString().Equals("System.Nullable`1[System.Int32]"))
                                                {
                                                    var valueTmp = result[prop.Name];
                                                    if (valueTmp != null)
                                                    {
                                                        var value = int.Parse(valueTmp.ToString());
                                                        prop.SetValue(obj, value, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(obj, 0, null);
                                                    }
                                                }
                                                else if (prop.PropertyType.ToString().Equals("System.Nullable`1[System.Boolean]"))
                                                {
                                                    var valueTmp = result[prop.Name];
                                                    if (valueTmp != null)
                                                    {
                                                        var value = bool.Parse(valueTmp.ToString());
                                                        prop.SetValue(obj, value, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(obj, 0, null);
                                                    }
                                                }
                                                else
                                                {
                                                    var value = Convert.ChangeType(result[prop.Name], prop.PropertyType);
                                                    prop.SetValue(obj, value, null);
                                                }

                                            }
                                        }
                                    }
                                }
                            }

                            list.Add(obj);
                        }
                        return list;

                    }
                }
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return entities.AsQueryable();
        }

        public TEntity GetById(object id)
        {
            return entities.Find(id);
        }

        public IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> filter)
        {
            return entities.Where(filter);
        }

        public IQueryable<TEntity> GetEntities()
        {
            return entities;
        }

        public IQueryable<TEntity> GetEntitiesWithInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.entities.Where(filter);

            return includes.Aggregate(query, (current, inc) => current.Include(inc));
        }

        public IQueryable<TEntity> GetEntitiesWithInclude(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.entities;

            return includes.Aggregate(query, (current, inc) => current.Include(inc));
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return entities.First(predicate);
        }

        public TEntity GetFirstOrDefault(Func<TEntity, bool> where)
        {
            return entities.Where(where).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return entities.Where(where).ToList();
        }

        public IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            if (where != null)
            {
                return entities.Where(where).AsQueryable();
            }

            return entities.AsQueryable();
        }

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return entities.Single(predicate);
        }

        public TEntity Insert(TEntity entityToInsert)
        {
            if (entityToInsert == null)
            {
                throw new ArgumentNullException("entityToInsert");
            }
            entities.Add(entityToInsert);
            return entityToInsert;
        }

        public List<TEntity> Insert(List<TEntity> entitiesToInsert)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("entityToUpdate");
            }
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }
    }
}