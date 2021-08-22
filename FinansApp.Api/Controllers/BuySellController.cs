using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FinansApp.Api.Helpers;
using FinansApp.Api.Models;
using FinansApp.Api.ReturnObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;

namespace FinansApp.Api.Controllers
{
    [ApiController]
    public class BuySellController : ControllerBase
    {
        [HttpGet("kurlari-getir")]
        public async Task<JsonReturnObject> GetBuySell(int type)
        //public JsonReturnObject GetBuySell()
        {
            var retObj = new JsonReturnObject();
            if (type == 1)
            {
                var http = new HttpClient();
                http.DefaultRequestHeaders.Add("token", "_magic");
                http.DefaultRequestHeaders.Host = "finans.apipara.com";
                http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
                var response = await http.GetAsync("https://finans.apipara.com/json/v9/market?coin=1");
                var content = response.Content;
                var result = await content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<Root>(result);
                retObj.DO = root;
            }
            if (type == 2)
            {
                var http = new HttpClient();
                http.DefaultRequestHeaders.Add("token", "_magic");
                http.DefaultRequestHeaders.Host = "finans.apipara.com";
                http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
                var response = await http.GetAsync("https://finans.apipara.com/json/v9/stock?all=1");
                var content = response.Content;
                var result = await content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<Root2>(result);
                retObj.DO = root;
            }
            return retObj;
        }

        [HttpGet("grafik")]
        public async Task<JsonReturnObject> GetChart(string code, string type, string time)
        {
            var retObj = new JsonReturnObject();

            var http = new HttpClient();
            var str = "https://finans.apipara.com/json/v9/stats?code=" + code + "&type=" + type + "&time=" + time;
            http.DefaultRequestHeaders.Add("token", "_magic");
            http.DefaultRequestHeaders.Host = "finans.apipara.com";
            http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
            var response = await http.GetAsync(str);
            var content = response.Content;
            var result = await content.ReadAsStringAsync();
            var root = JsonConvert.DeserializeObject<Root3>(result);
            retObj.DO = root;
            return retObj;
        }
    }


    public class Response3
    {
        public double selling { get; set; }
        public double latest { get; set; }
        public string update_date { get; set; }
        public bool show { get; set; }
    }

    public class Root3
    {
        public List<Response3> response { get; set; }
    }

    public class Udate
    {
        public int sec { get; set; }
        public int usec { get; set; }
    }

    public class AlarmProps
    {
        public double step { get; set; }
        public double min { get; set; }
        public double max { get; set; }
    }

    public class Currency
    {
        public double buying { get; set; }
        public double change_rate { get; set; }
        public string code { get; set; }
        public int currency { get; set; }
        public string full_name { get; set; }
        public double? latest { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public string name { get; set; }
        public double selling { get; set; }
        public string src { get; set; }
        public Udate udate { get; set; }
        public string utime { get; set; }
        public AlarmProps alarmProps { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public string shortName { get; set; }
        public string id { get; set; }
        public int cell { get; set; }
        public string day { get; set; }
    }

    public class Gold
    {
        public double buying { get; set; }
        public double change_rate { get; set; }
        public string code { get; set; }
        public string full_name { get; set; }
        public object gold { get; set; }
        public double latest { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public string name { get; set; }
        public double selling { get; set; }
        public string src { get; set; }
        public Udate udate { get; set; }
        public string utime { get; set; }
        public AlarmProps alarmProps { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public string shortName { get; set; }
        public string id { get; set; }
        public int cell { get; set; }
        public string day { get; set; }
        public int? sort { get; set; }
        public string size { get; set; }
        public string unit { get; set; }
        public string android { get; set; }
        public string text { get; set; }
        public string status { get; set; }
        public string h { get; set; }
    }

    public class Exchange
    {
        public double bchange { get; set; }
        public string bcode { get; set; }
        public string bname { get; set; }
        public int brank { get; set; }
        public double buying { get; set; }
        public double bvolume { get; set; }
        public double change_rate { get; set; }
        public string code { get; set; }
        public string code1 { get; set; }
        public string code2 { get; set; }
        public bool coin { get; set; }
        public string fullName { get; set; }
        public string full_name { get; set; }
        public double? latest { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public string name { get; set; }
        public double selling { get; set; }
        public Udate udate { get; set; }
        public string utime { get; set; }
        public AlarmProps alarmProps { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public string shortName { get; set; }
        public string id { get; set; }
        public int cell { get; set; }
        public string change { get; set; }
        public string day { get; set; }
        public string time { get; set; }
        public object bank { get; set; }
    }

    public class Img
    {
        public string url { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Action
    {
        public string page { get; set; }
    }

    public class Emtia
    {
        public double buying { get; set; }
        public double change_rate { get; set; }
        public string code { get; set; }
        public string full_name { get; set; }
        public double latest { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public string name { get; set; }
        public double selling { get; set; }
        public int show { get; set; }
        public int sort { get; set; }
        public Udate udate { get; set; }
        public string utime { get; set; }
        public AlarmProps alarmProps { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public string shortName { get; set; }
        public string id { get; set; }
        public int cell { get; set; }
        public string size { get; set; }
        public string unit { get; set; }
        public string android { get; set; }
        public Img img { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string button { get; set; }
        public bool? buttonshow { get; set; }
        public Action action { get; set; }
        public string text { get; set; }
        public string status { get; set; }
        public string h { get; set; }
    }

    public class Endex
    {
        public double buying { get; set; }
        public double change_rate { get; set; }
        public string code { get; set; }
        public string day { get; set; }
        public string full_name { get; set; }
        public double latest { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public bool nonmarket { get; set; }
        public double selling { get; set; }
        public int sort { get; set; }
        public Udate udate { get; set; }
        public string utime { get; set; }
        public AlarmProps alarmProps { get; set; }
        public List<object> values { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public string shortName { get; set; }
        public string id { get; set; }
        public int cell { get; set; }
    }

    public class Coin
    {
        public double bchange { get; set; }
        public string bcode { get; set; }
        public string bname { get; set; }
        public int brank { get; set; }
        public double buying { get; set; }
        public double bvolume { get; set; }
        public double change_rate { get; set; }
        public string code { get; set; }
        public string code1 { get; set; }
        public string code2 { get; set; }
        public bool coin { get; set; }
        public string fullName { get; set; }
        public string full_name { get; set; }
        public double? latest { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public string name { get; set; }
        public double selling { get; set; }
        public Udate udate { get; set; }
        public string utime { get; set; }
        public AlarmProps alarmProps { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public string shortName { get; set; }
        public string id { get; set; }
        public int cell { get; set; }
        public string change { get; set; }
        public string day { get; set; }
        public string time { get; set; }
        public object bank { get; set; }
    }

    public class Response
    {
        public List<Currency> currency { get; set; }
        public List<Gold> gold { get; set; }
        public List<Exchange> exchange { get; set; }
        public List<Emtia> emtia { get; set; }
        public List<Endex> endex { get; set; }
        public List<Coin> coin { get; set; }
    }

    public class Root
    {
        public Response response { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Filter
    {
        public string title { get; set; }
        public string sortby { get; set; }
        public string dir { get; set; }
    }

    public class AlarmProps2
    {
        public double step { get; set; }
        public double min { get; set; }
        public double max { get; set; }
    }

    public class Item
    {
        public double buying { get; set; }
        public double ceil { get; set; }
        public double change { get; set; }
        public double change_rate { get; set; }
        public string code { get; set; }
        public double difference { get; set; }
        public double floor { get; set; }
        public string full_name { get; set; }
        public double highest { get; set; }
        public double latest { get; set; }
        public double lowest { get; set; }
        public string market { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public double net { get; set; }
        public double selling { get; set; }
        public double supply { get; set; }
        public int uptime { get; set; }
        public string utime { get; set; }
        public AlarmProps2 alarmProps { get; set; }
        public string type { get; set; }
        public string subtitle { get; set; }
        public int cell { get; set; }
        public string shortName { get; set; }
        public string id { get; set; }
    }

    public class Response2
    {
        public List<Filter> filter { get; set; }
        public List<Item> items { get; set; }
        public bool more { get; set; }
    }

    public class Root2
    {
        public Response2 response { get; set; }
    }




}