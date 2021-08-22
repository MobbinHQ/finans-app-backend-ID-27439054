using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinansApp.Data.Tables
{
    public class Alert
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 1-> Altına Gelince , 2-> Üstüne Çıkınca
        /// </summary>
        public int AlertType { get; set; }
        /// <summary>
        /// Ör : USDTRY
        /// </summary>
        public string BuySell { get; set; }
        [DataType("decimal(16 ,8)")]
        public decimal Limit { get; set; }
        public bool IsActive { get; set; }
    }
}
