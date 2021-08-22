using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Business.Signals.Dto
{
    public class SignalAddOrEditDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string BuySell { get; set; }
        public string Entry { get; set; }
        public string TP { get; set; }
        public string SL { get; set; }
    }
}
