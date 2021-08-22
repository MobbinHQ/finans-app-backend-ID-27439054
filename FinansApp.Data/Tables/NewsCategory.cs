using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Data.Tables
{
    public class NewsCategory
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
