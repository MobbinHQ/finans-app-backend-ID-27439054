using System;
using System.Collections.Generic;
using System.Text;

namespace FinansApp.Data.Tables
{
    public class StaticPage
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
    }
}
