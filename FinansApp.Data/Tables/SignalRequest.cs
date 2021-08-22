using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Data.Tables
{
    public class SignalRequest
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
