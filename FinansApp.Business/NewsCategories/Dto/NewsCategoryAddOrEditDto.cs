using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Business.NewsCategories.Dto
{
    public class NewsCategoryAddOrEditDto
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<NewsCategory> NewsCategories { get; set; }
    }
}
