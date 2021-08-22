using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Web.ReturnObjects
{
    public class JsonReturnObject
    {
        public JsonReturnObject()
        {
            EC = 0;
            MSG = "";
            DO = null;
        }

        public int EC { get; set; }
        public string MSG { get; set; }
        public object DO { get; set; }
    }
}
