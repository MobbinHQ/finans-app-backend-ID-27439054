using System;
namespace FinansApp.Business.Portfoy.Dto
{
    public class PortfoyAddDto
    {
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}
