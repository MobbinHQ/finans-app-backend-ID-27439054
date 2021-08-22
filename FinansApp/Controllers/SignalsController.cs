using FinansApp.Business.Signals;
using FinansApp.Business.Signals.Dto;
using FinansApp.Models.Signals;
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
    public class SignalsController : Controller
    {
        private readonly ISignalService _signalService;

        public SignalsController(ISignalService signalService)
        {
            _signalService = signalService;
        }
        [HttpGet("sinyaller")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("sinyal-liste")]
        public async Task<JsonResult> GetList(int Start, int Length)
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var response = await _signalService.GetFiltered(Start, Length, searchValue);
            //var restCategories = await _restCategoryService.GetList();
            var m = new SignalViewModel();
            var l = new List<SModel>();
            foreach (var item in response)
            {
                var rm = new SModel()
                {
                    CreateDate = item.CreateDate,
                    Id = item.Id,
                    BuySell = item.BuySell,
                    Entry = item.Entry,
                    SL = item.SL,
                    TP = item.TP
                };
                l.Add(rm);
            }
            m.SModels = l;
            var jsonData = new { recordsFiltered = await _signalService.GetFilteredCount(searchValue), recordsTotal = m.SModels.Count, data = m.SModels };
            return Json(jsonData);
        }


        [HttpGet("sinyal-ekle")]
        [HttpGet("sinyal-duzenle/{id}")]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var dto = new SignalAddOrEditDto();
            if (id.HasValue)
            {
                var signal = await _signalService.Get(id.Value);
                dto.Id = signal.Id;
                dto.Type = signal.Type;
                dto.BuySell = signal.BuySell;
                dto.Entry = signal.Entry;
                dto.SL = signal.SL;
                dto.TP = signal.TP;
            }
            return View(dto);
        }
        [HttpPost("sinyal-ekle")]
        [HttpPost("sinyal-duzenle/{id}")]
        public async Task<JsonResult> AddOrEdit(SignalAddOrEditDto dto)
        {
            var response = new JsonReturnObject();
            response.EC = await _signalService.AddOrEdit(dto);
            return Json(response);
        }
        [HttpPost("sinyal-sil")]
        public async Task<JsonResult> Delete(int id)
        {
            var response = new JsonReturnObject();
            response.EC = await _signalService.Delete(id);
            return Json(response);
        }
    }
}
