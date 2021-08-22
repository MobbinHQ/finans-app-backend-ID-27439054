using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinansApp.Api.Helpers;
using FinansApp.Api.ReturnObjects;
using FinansApp.Business.Alerts;
using FinansApp.Business.Alerts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinansApp.Api.Controllers
{
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;
            
        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }
        [Authorize]
        [HttpPost("alarm-ekle")]
        public async Task<JsonReturnObject> AddAlert([FromBody]AddAlertDto AlertDto)
        {
            var retObj = new JsonReturnObject();
            var userId = int.Parse(HttpContext.Items["ID"].ToString());
            var response = await _alertService.Add(AlertDto,userId);
            retObj.EC = response;
            retObj.MSG = response == 0 ? "Alarm Başarıyla Eklendi." : "Alarm Eklerken Hata Oluştu.";
            return retObj;
        }
        [Authorize]
        [HttpGet("alarmlar")]
        public async Task<JsonReturnObject> GetAlerts()
        {
            var retObj = new JsonReturnObject();
            var userId = int.Parse(HttpContext.Items["ID"].ToString());
            var alerts = await _alertService.GetListByUserId(userId);
            retObj.DO = alerts;
            return retObj;
        }

        [Authorize]
        [HttpPost("alarm-sil")]
        public async Task<bool> DeleteAlert(int id)
        {
            return await _alertService.DeleteAlert(id);
        }
    }
}