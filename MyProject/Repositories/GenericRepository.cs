using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }
    }
}