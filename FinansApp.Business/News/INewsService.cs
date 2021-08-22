using FinansApp.Business.News.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.News
{
    public interface INewsService
    {
        Task<int> AddOrEdit(NewsAddOrEditDto dto);
        Task<int> Delete(int id);
        Task<Data.Tables.News> Get(int id);
        Task<List<Data.Tables.News>> GetAllPaging(int skip, int take);
        Task<List<Data.Tables.News>> GetAllPagingByCategoryId(int skip, int take, int categoryId);
        Task<List<Data.Tables.News>> GetFiltered(int skip, int take, string search);
        Task<int> GetFilteredCount(string search);
        Task<List<Data.Tables.News>> GetList();
    }
}
