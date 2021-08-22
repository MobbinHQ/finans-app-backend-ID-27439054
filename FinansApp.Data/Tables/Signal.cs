using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Data.Tables
{
    public class Signal
    {
        public int Id { get; set; }
        /// <summary>
        /// 1 - Al , 2 - Sat
        /// </summary>
        public int Type { get; set; }
        public string BuySell { get; set; }
        public string Entry { get; set; }
        public string TP { get; set; }
        public string SL { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
