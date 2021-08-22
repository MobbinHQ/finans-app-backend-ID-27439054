using FinansApp.Business.News;
using FinansApp.Business.News.Dto;
using FinansApp.Business.NewsCategories;
using FinansApp.Models.News;
using FinansApp.Web.ReturnObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly INewsCategoryService _newsCategoryService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string AppBaseUrl => $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host}{this.HttpContext.Request.PathBase}";
        public NewsController(INewsService newsService, INewsCategoryService newsCategoryService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _newsService = newsService;
            _newsCategoryService = newsCategoryService;
        }

        [HttpGet("haberler")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("haber-liste")]
        public async Task<JsonResult> GetList(int Start, int Length)
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var response = await _newsService.GetFiltered(Start, Length, searchValue);
            //var restCategories = await _restCategoryService.GetList();
            var m = new NewsViewModel();
            var l = new List<NCModel>();
            foreach (var item in response)
            {
                var rm = new NCModel()
                {
                    CreateDate = item.CreateDate,
                    Id = item.Id,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    Title = item.Title
                };
                l.Add(rm);
            }
            m.PCModels = l;
            var jsonData = new { recordsFiltered = await _newsService.GetFilteredCount(searchValue), recordsTotal = m.PCModels.Count, data = m.PCModels };
            return Json(jsonData);
        }

        [HttpGet("haber-ekle")]
        [HttpGet("haber-duzenle/{id}")]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var dto = new NewsAddOrEditDto();
            if (id.HasValue)
            {
                var news = await _newsService.Get(id.Value);
                dto.Id = news.Id;
                dto.IsActive = news.IsActive;
                dto.CategoryId = news.CategoryId;
                dto.Content = news.Content;
                dto.Description = news.Description;
                dto.ImageUrl = news.ImageUrl;
                dto.Title = news.Title;
                dto.PublishDate = news.PublishDate;
                dto.FinishDate = news.FinishDate;
                dto.ImageUrl = news.ImageUrl;
            }
            dto.Categories = await _newsCategoryService.GetList();
            return View(dto);
        }
        [HttpPost("haber-ekle")]
        [HttpPost("haber-duzenle/{id}")]
        public async Task<JsonResult> AddOrEdit(NewsAddOrEditDto dto)
        {
            var response = new JsonReturnObject();
            if (dto.ImageFile != null)
            {
                string uniqueFileName = UploadedFile(dto.ImageFile);
                dto.ImageUrl = AppBaseUrl + "/UploadedImages/" + uniqueFileName;
            }
            response.EC = await _newsService.AddOrEdit(dto);
            return Json(response);
        }
        [HttpPost("haber-sil")]
        public async Task<JsonResult> Delete(int id)
        {
            var response = new JsonReturnObject();
            response.EC = await _newsService.Delete(id);
            return Json(response);
        }

        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "UploadedImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
