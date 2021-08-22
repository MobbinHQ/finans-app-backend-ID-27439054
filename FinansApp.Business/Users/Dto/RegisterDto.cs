using FinansApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Business.Users.Dto
{
    public class RegisterDto
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string OneSignalId { get; set; }
    }
}
