using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Models.Signals
{
    public class SignalViewModel
    {
        public List<SModel> SModels { get; set; }
    }

    public class SModel
    {
        public int Id { get; set; }
        public string BuySell { get; set; }
        public string Entry { get; set; }
        public string TP { get; set; }
        public string SL { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
