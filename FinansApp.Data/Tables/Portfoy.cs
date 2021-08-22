using System;
namespace FinansApp.Data.Tables
{
    public class Portfoy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
