using FinansApp.Api.Helpers;
using FinansApp.Api.ReturnObjects;
using FinansApp.Business.SignalRequests;
using FinansApp.Business.SignalRequests.Dto;
using FinansApp.Business.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Api.Controllers
{
    
    [ApiController]
    public class SignalRequestsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISignalRequestService _signalRequestService;

        public SignalRequestsController(IUserService userService, ISignalRequestService signalRequestService)
        {
            _userService = userService;
            _signalRequestService = signalRequestService;
        }
        [HttpPost("addSignalRequest")]
        public async Task<bool> Register([FromBody] SignalRequestDto dto) => await _signalRequestService.Add(dto);

        
    }
}
