using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DataModels
{
    public class Category
    {
        public int CaterogyId { get; set; }
        public string CaterogyName { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
