using FinansApp.Business.StaticPages;
using FinansApp.Business.StaticPages.Dto;
using FinansApp.Models.StaticPages;
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
    public class StaticPagesController : Controller
    {
        private readonly IStaticPageService _staticPageService;

        public StaticPagesController(IStaticPageService staticPageService)
        {
            _staticPageService = staticPageService;
        }
        [HttpGet("statik-sayfalar")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("sayfalar-liste")]
        public async Task<JsonResult> GetList(int Start, int Length)
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var response = await _staticPageService.GetFiltered(Start, Length, searchValue);
            var m = new StaticPagesModel();
            var l = new List<SModel>();
            foreach (var item in response)
            {
                var pcm = new SModel()
                {
                    Name = item.Name,
                    //Text = item.Text,
                    Id = item.Id
                };
                l.Add(pcm);
            }
            m.PCModels = l;
            var jsonData = new { recordsFiltered = await _staticPageService.GetFilteredCount(searchValue), recordsTotal = m.PCModels.Count, data = m.PCModels };
            return Json(jsonData);
        }

        [HttpGet("sayfa-ekle")]
        [HttpGet("sayfa-duzenle/{id}")]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var dto = new StaticPageAddOrEditDto();
            if (id.HasValue)
            {
                var productCategory = await _staticPageService.GetById(id.Value);
                dto.Id = productCategory.Id;
                dto.Name = productCategory.Name;
                dto.Text = productCategory.Text;
            }
            //dto.ProductCategories = await _productCategoryService.GetList();
            return View(dto);
        }
        [HttpPost("sayfa-ekle")]
        [HttpPost("sayfa-duzenle/{id}")]
        public async Task<JsonResult> AddOrEdit(StaticPageAddOrEditDto dto)
        {
            var response = new JsonReturnObject();
            response.EC = await _staticPageService.AddOrEdit(dto);
            return Json(response);
        }

        [HttpPost("icerik-kaydet")]
        public async Task<JsonResult> Save(int id, string text)
        {
            var response = await _staticPageService.EditText(id, text);
            return Json(response);
        }

        [HttpPost("sayfa-sil")]
        public async Task<JsonResult> Delete(int id)
        {
            var response = new JsonReturnObject();
            response.EC = await _staticPageService.Delete(id);
            return Json(response);
        }
    }
}
