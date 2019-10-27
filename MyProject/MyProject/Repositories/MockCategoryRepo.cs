using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProject.DataModels;

namespace MyProject.Repositories
{
    public class MockCategoryRepo : ICategoryRepo
    {
        private static List<Category> categories = new List<Category>()
        {
            new Category{CaterogyId = 1, CaterogyName = "Bia"},
            new Category{CaterogyId = 11, CaterogyName = "Bia 333", ParentCategoryId = 1},
            new Category{CaterogyId = 12, CaterogyName = "Bia Heneiken", ParentCategoryId = 1},
            new Category{CaterogyId = 13, CaterogyName = "Bia Sài Gòn", ParentCategoryId = 1},
                new Category{CaterogyId = 2, CaterogyName = "Nước ngọt"},
                new Category{CaterogyId = 21, CaterogyName = "Pepsi", ParentCategoryId = 2},
                new Category{CaterogyId = 22, CaterogyName = "7up", ParentCategoryId = 2},
                new Category{CaterogyId = 3, CaterogyName = "Điện máy"},
                new Category{CaterogyId = 31, CaterogyName = "Điện thoại", ParentCategoryId = 3},
                new Category{CaterogyId = 32, CaterogyName = "Máy tính bảng", ParentCategoryId = 3},
                new Category{CaterogyId = 33, CaterogyName = "Laptop", ParentCategoryId = 3},
        };

        public IEnumerable<Category> GetAll()
        {
            return categories;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Category category)
        {
            throw new NotImplementedException();
        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
