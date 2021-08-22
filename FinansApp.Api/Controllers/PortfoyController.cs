using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinansApp.Api.Helpers;
using FinansApp.Api.ReturnObjects;
using FinansApp.Business.Portfoy;
using FinansApp.Business.Portfoy.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinansApp.Api.Controllers
{
    [ApiController]
    public class PortfoyController : ControllerBase
    {
        private readonly IPortfoyService _portfoyService;

        public PortfoyController(IPortfoyService portfoyService)
        {
            _portfoyService = portfoyService;
        }
        [Authorize]
        [HttpGet("portfoy")]
        public async Task<JsonReturnObject> GetPortfoy()
        {
            var retObj = new JsonReturnObject();
            var userId = int.Parse(HttpContext.Items["ID"].ToString());
            var response = await _portfoyService.GetListByUserId(userId);
            retObj.DO = response;
            return retObj;
        }
        [Authorize]
        [HttpPost("portfoy-ekle")]
        public async Task<bool> AddPortfoy(PortfoyAddDto dto)
        {
            var userId = int.Parse(HttpContext.Items["ID"].ToString());
            return await _portfoyService.AddPortfoy(dto, userId);
        }
        [Authorize]
        [HttpPost("portfoy-sil")]
        public async Task<bool> AddPortfoy(int id)
        {
            var userId = int.Parse(HttpContext.Items["ID"].ToString());
            return await _portfoyService.DeletePortfoy(id);
        }
    }
}