using FinansApp.Business.StaticPages.Dto;
using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.StaticPages
{
    public interface IStaticPageService
    {
        Task<int> AddOrEdit(StaticPageAddOrEditDto dto);
        Task<int> Delete(int id);
        Task<bool> EditText(int id, string text);
        Task<StaticPage> GetById(int id);
        Task<List<StaticPage>> GetFiltered(int skip, int take, string search);
        Task<int> GetFilteredCount(string search);
    }
}
