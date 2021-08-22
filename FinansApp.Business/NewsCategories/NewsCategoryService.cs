using FinansApp.Business.NewsCategories.Dto;
using FinansApp.Data;
using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.NewsCategories
{
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly FinansAppDbContext _context;

        public NewsCategoryService(FinansAppDbContext context)
        {
            _context = context;
        }

        public async Task<NewsCategory> Get(int id)
        {
            return await _context.NewsCategories.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<NewsCategory>> GetList()
        {
            return await _context.NewsCategories.ToListAsync();
        }
        public async Task<List<NewsCategory>> GetListForMobile()
        {
            return await _context.NewsCategories.Where(x=>x.IsActive).ToListAsync();
        }
        public async Task<List<NewsCategory>> GetMainCategories()
        {
            return await _context.NewsCategories.Where(x => x.ParentCategoryId == null && x.IsActive).ToListAsync();
        }
        public async Task<List<NewsCategory>> GetSubCategories(int parentCategoryId)
        {
            return await _context.NewsCategories.Where(x => x.IsActive && x.ParentCategoryId == parentCategoryId).ToListAsync();
        }

        public async Task<List<NewsCategory>> GetFiltered(int skip, int take, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.NewsCategories.Where(x => x.Name.Contains(search)).OrderBy(x => x.Name).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.NewsCategories.OrderBy(x => x.Name).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> GetFilteredCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.NewsCategories.Where(x => x.Name.Contains(search)).OrderBy(x => x.Name).CountAsync();
            }
            return await _context.NewsCategories.OrderBy(x => x.Name).CountAsync();
        }
        public async Task<int> AddOrEdit(NewsCategoryAddOrEditDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    var productCategory = new NewsCategory
                    {
                        IsActive = dto.IsActive,
                        Name = dto.Name,
                        ParentCategoryId = dto.ParentCategoryId
                    };
                    await _context.NewsCategories.AddAsync(productCategory);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var productCategory = await _context.NewsCategories.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    productCategory.IsActive = dto.IsActive;
                    productCategory.Name = dto.Name;
                    productCategory.ParentCategoryId = dto.ParentCategoryId;
                    _context.NewsCategories.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                // TODO : News işlemleri yapıldıktan sonra kategori silme esnasında bu kategoride ürün var mı kontrolü yapılacak.
                //var p = await _context.Products.CountAsync(x => x.CategoryId == id);
                //if (p > 0)
                //{
                //    return 1;
                //}
                var pcs = await _context.NewsCategories.CountAsync(x => x.ParentCategoryId == id);
                if (pcs > 0)
                {
                    return 2;
                }
                var pc = await _context.NewsCategories.FirstOrDefaultAsync(x => x.Id == id);
                _context.NewsCategories.Remove(pc);
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
