using Microsoft.EntityFrameworkCore;
using System;

namespace Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        DbContext GetDbContext();
        bool Save();
        IGenericRepository<T> Respository<T>() where T : class;
    }
}
