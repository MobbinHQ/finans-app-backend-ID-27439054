using FinansApp.Business.SignalRequests;
using FinansApp.Models.SignalRequests;
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
    public class SignalRequestsController : Controller
    {
        private readonly ISignalRequestService _signalRequestService;

        public SignalRequestsController(ISignalRequestService signalRequestService)
        {
            _signalRequestService = signalRequestService;
        }
        [HttpGet("sinyal-istekleri")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("sinyal-istek-liste")]
        public async Task<JsonResult> GetList(int Start, int Length)
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var response = await _signalRequestService.GetFiltered(Start, Length, searchValue);
            //var restCategories = await _restCategoryService.GetList();
            var m = new SignalRequestViewModel();
            var l = new List<SRModel>();
            foreach (var item in response)
            {
                var rm = new SRModel()
                {
                    CreateDate = item.CreateDate,
                    Id = item.Id,
                    Email = item.Email,
                    NameSurname = item.NameSurname,
                    PhoneNumber = item.PhoneNumber
                };
                l.Add(rm);
            }
            m.SModels = l;
            var jsonData = new { recordsFiltered = await _signalRequestService.GetFilteredCount(searchValue), recordsTotal = m.SModels.Count, data = m.SModels };
            return Json(jsonData);
        }

        [HttpPost("sinyal-istek-sil")]
        public async Task<JsonResult> Delete(int id)
        {
            var response = new JsonReturnObject();
            response.EC = await _signalRequestService.Delete(id);
            return Json(response);
        }
    }
}
