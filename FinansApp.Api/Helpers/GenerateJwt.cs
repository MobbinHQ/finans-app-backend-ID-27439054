using FinansApp.Api.Helpers;
using FinansApp.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Api.Helpers
{
    public class GenerateJwt : IGenerateJwt
    {
        // private readonly AppSettings _appSettings;
        public IConfiguration _conf;

        public GenerateJwt(IConfiguration conf)
        {
            _conf = conf;
            // _appSettings = appSettings.Value;
        }

        public string generateJwtToken(User user)
        {
            try
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var t = _conf.GetValue<String>("AppSettings:Secret");
                var key = Encoding.UTF8.GetBytes(t);
                // var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
                var claims = new List<Claim>{
                            //new Claim(ClaimTypes.Name, user.Name + " " + user.Surname),
                            //new Claim(ClaimTypes.Email,user.Email),
                            new Claim(ClaimTypes.Sid,user.Id.ToString())
                        };
                var userIdentity = new ClaimsIdentity(claims, "login");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddYears(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (System.Exception)
            {
                return null;
                throw;
            }

        }
    }
}
