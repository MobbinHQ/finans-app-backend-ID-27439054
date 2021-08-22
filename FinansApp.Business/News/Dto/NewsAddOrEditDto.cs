using FinansApp.Data.Tables;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Business.News.Dto
{
    public class NewsAddOrEditDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime FinishDate { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<NewsCategory> Categories { get; set; }
    }
}
