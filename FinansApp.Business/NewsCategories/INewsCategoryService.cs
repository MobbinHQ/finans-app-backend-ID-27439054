using FinansApp.Business.NewsCategories.Dto;
using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.NewsCategories
{
    public interface INewsCategoryService
    {
        Task<int> AddOrEdit(NewsCategoryAddOrEditDto dto);
        Task<int> Delete(int id);
        Task<NewsCategory> Get(int id);
        Task<List<NewsCategory>> GetFiltered(int skip, int take, string search);
        Task<int> GetFilteredCount(string search);
        Task<List<NewsCategory>> GetList();
        Task<List<NewsCategory>> GetListForMobile();
        Task<List<NewsCategory>> GetMainCategories();
        Task<List<NewsCategory>> GetSubCategories(int parentCategoryId);
    }
}
