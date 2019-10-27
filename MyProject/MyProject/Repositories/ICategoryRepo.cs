using MyProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Repositories
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Delete(int id);
        void Update(Category category);
        void Insert(Category category);
    }
}
