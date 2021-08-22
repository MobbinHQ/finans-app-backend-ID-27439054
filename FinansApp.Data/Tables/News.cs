using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Data.Tables
{
    public class News
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
