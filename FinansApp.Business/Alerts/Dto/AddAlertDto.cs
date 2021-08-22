using System;
namespace FinansApp.Business.Alerts.Dto
{
    public class AddAlertDto
    {
        public int Id { get; set; }
        public int AlertType { get; set; }
        public string BuySell { get; set; }
        public decimal Limit { get; set; }
    }
}
