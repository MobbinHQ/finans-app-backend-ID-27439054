using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinansApp.Api.ReturnObjects;
using FinansApp.Business.NewsCategories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinansApp.Api.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly INewsCategoryService _newsCategoryService;

        public CategoriesController(INewsCategoryService newsCategoryService)
        {
            _newsCategoryService = newsCategoryService;
        }

        [HttpGet("ana-kategoriler")]
        public async Task<JsonReturnObject> GetMainCategories()
        {
            var retObj = new JsonReturnObject();
            var categories = await _newsCategoryService.GetMainCategories();
            retObj.DO = categories;
            return retObj;
        }

        [HttpGet("alt-kategoriler")]
        public async Task<JsonReturnObject> GetSubCategories(int id)
        {
            var retObj = new JsonReturnObject();
            var categories = await _newsCategoryService.GetSubCategories(id);
            retObj.DO = categories;
            return retObj;
        }
    }
}