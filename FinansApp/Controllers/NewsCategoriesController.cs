using FinansApp.Business.NewsCategories;
using FinansApp.Business.NewsCategories.Dto;
using FinansApp.Models.NewsCategories;
using FinansApp.Web.ReturnObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Controllers
{
    [Authorize]
    public class NewsCategoriesController : Controller
    {
        private readonly INewsCategoryService _newsCategoryService;
        public NewsCategoriesController(INewsCategoryService newsCategoryService)
        {
            _newsCategoryService = newsCategoryService;
        }
        [HttpGet("haber-kategorileri")]
        public async Task<IActionResult> Index()
        {
            var model = new NewsCategoryListDto();
            model.NewsCategories = await _newsCategoryService.GetList();
            return View(model);
        }
        [HttpPost("haber-kategori-liste")]
        public async Task<JsonResult> GetList(int Start, int Length)
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var response = await _newsCategoryService.GetFiltered(Start, Length, searchValue);
            var productCategories = await _newsCategoryService.GetList();
            var m = new NewsCategoryViewModel();
            var l = new List<NCModel>();
            foreach (var item in response)
            {
                var pcm = new NCModel()
                {
                    CategoryName = productCategories.FirstOrDefault(x => x.Id == item.Id).Name,
                    IsActive = item.IsActive,
                    ParentCategoryName = !item.ParentCategoryId.HasValue ? "" : productCategories.FirstOrDefault(x => x.Id == item.ParentCategoryId).Name,
                    Id = item.Id
                };
                l.Add(pcm);
            }
            m.PCModels = l;
            var jsonData = new { recordsFiltered = await _newsCategoryService.GetFilteredCount(searchValue), recordsTotal = m.PCModels.Count, data = m.PCModels };
            return Json(jsonData);
        }
        [HttpGet("haber-kategori-ekle")]
        [HttpGet("haber-kategori-duzenle/{id}")]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var dto = new NewsCategoryAddOrEditDto();
            if (id.HasValue)
            {
                var productCategory = await _newsCategoryService.Get(id.Value);
                dto.Id = productCategory.Id;
                dto.IsActive = productCategory.IsActive;
                dto.Name = productCategory.Name;
                dto.ParentCategoryId = productCategory.ParentCategoryId;
            }
            dto.NewsCategories = await _newsCategoryService.GetList();
            return View(dto);
        }
        [HttpPost("haber-kategori-ekle")]
        [HttpPost("haber-kategori-duzenle/{id}")]
        public async Task<JsonResult> AddOrEdit(NewsCategoryAddOrEditDto dto)
        {
            var response = new JsonReturnObject();
            response.EC = await _newsCategoryService.AddOrEdit(dto);
            return Json(response);
        }
        [HttpPost("haber-kategori-sil")]
        public async Task<JsonResult> Delete(int id)
        {
            var response = new JsonReturnObject();
            response.EC = await _newsCategoryService.Delete(id);
            return Json(response);
        }
    }
}
