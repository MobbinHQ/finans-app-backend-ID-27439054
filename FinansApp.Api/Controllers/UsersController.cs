using FinansApp.Api.Helpers;
using FinansApp.Api.ReturnObjects;
using FinansApp.Business.Users;
using FinansApp.Business.Users.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IGenerateJwt _generateJjwt;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UsersController(IUserService userService, IConfiguration configuration, IGenerateJwt generateJjwt, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _generateJjwt = generateJjwt;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<JsonReturnObject> RegisterAsync([FromBody] RegisterDto dto)
        {
            var retObj = new JsonReturnObject();
            var user = await _userService.EmailControl(dto.Email);
            if (user != null)
            {
                retObj.EC = -1;
                retObj.MSG = "Email kullanımda.";
                return retObj;
            }
            else
            {
                var retVal = await _userService.RegisterMobile(dto);
                var token = new Token.Token();
                token.token = _generateJjwt.generateJwtToken(retVal);
                retObj.DO = token;
                retObj.MSG = "Üyelik işlemi başarılı";
                return retObj;
            }
        }

        [HttpPost("login")]
        public async Task<JsonReturnObject> LoginAsync([FromBody] LoginDto dto)
        {
            var retObj = new JsonReturnObject();
            var user = await _userService.Login(dto);
            if (user == null)
            {
                retObj.EC = -1;
                retObj.MSG = "Kullanıcı bulunamadı.";
            }
            else
            {
                var token = new Token.Token();
                token.token = _generateJjwt.generateJwtToken(user);
                retObj.DO = token;
                retObj.MSG = "Giriş başarılı";
            }
            return retObj;
        }
    }
}
