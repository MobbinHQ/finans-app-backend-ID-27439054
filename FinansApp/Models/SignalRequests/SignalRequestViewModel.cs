using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Models.SignalRequests
{
    public class SignalRequestViewModel
    {
        public List<SRModel> SModels { get; set; }
    }

    public class SRModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
