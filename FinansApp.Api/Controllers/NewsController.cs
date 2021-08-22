using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinansApp.Api.ReturnObjects;
using FinansApp.Business.News;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinansApp.Api.Controllers
{
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("tum-haberler")]
        public async Task<JsonReturnObject> GetAllNews(int skip, int take)
        {
            var retObj = new JsonReturnObject();
            var news = await _newsService.GetAllPaging(skip, take);
            retObj.DO = news;
            return retObj;
        }

        [HttpGet("haberler-kategori")]
        public async Task<JsonReturnObject> GetNewsByCategoryId(int skip, int take, int categoryId)
        {
            var retObj = new JsonReturnObject();
            var news = await _newsService.GetAllPagingByCategoryId(skip, take,categoryId);
            retObj.DO = news;
            return retObj;
        }
    }
}