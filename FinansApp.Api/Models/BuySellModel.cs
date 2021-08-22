namespace FinansApp.Api.Models
{
    public class BuySellModel
    {
        public string Name { get; set; }
        public BuySellValue Values { get; set; }
    }

    public class BuySellValue{
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
    }
}