using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Business.Users.Dto
{
    public class UserAddOrEditDto
    {
        public int Id { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
