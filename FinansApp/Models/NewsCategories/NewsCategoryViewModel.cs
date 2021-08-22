using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Models.NewsCategories
{
    public class NewsCategoryViewModel
    {
        public List<NCModel> PCModels { get; set; }
    }

    public class NCModel
    {
        public int Id { get; set; }
        public string ParentCategoryName { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
