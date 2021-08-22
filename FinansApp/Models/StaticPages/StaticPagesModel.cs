using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Models.StaticPages
{
    public class StaticPagesModel
    {
        public List<SModel> PCModels { get; set; }
    }
    public class SModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
