using FinansApp.Business.News.Dto;
using FinansApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.News
{
    public class NewsService : INewsService
    {
        private readonly FinansAppDbContext _context;

        public NewsService(FinansAppDbContext context)
        {
            _context = context;
        }

        public async Task<FinansApp.Data.Tables.News> Get(int id)
        {
            return await _context.News.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<FinansApp.Data.Tables.News>> GetList()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<List<FinansApp.Data.Tables.News>> GetAllPaging(int skip,int take)
        {
            return await _context.News.OrderByDescending(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<List<FinansApp.Data.Tables.News>> GetAllPagingByCategoryId(int skip, int take,int categoryId)
        {
            return await _context.News.Where(x=>x.CategoryId == categoryId).OrderByDescending(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<FinansApp.Data.Tables.News>> GetFiltered(int skip, int take, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.News.Where(x => x.Title.Contains(search)).OrderBy(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.News.OrderBy(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> GetFilteredCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.News.Where(x => x.Title.Contains(search)).CountAsync();
            }
            return await _context.News.CountAsync();
        }

        public async Task<int> AddOrEdit(NewsAddOrEditDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    var news = new FinansApp.Data.Tables.News
                    {
                        IsActive = dto.IsActive,
                        Title = dto.Title,
                        CategoryId = dto.CategoryId,
                        Content = dto.Content,
                        CreateDate = DateTime.Now,
                        Description = dto.Description,
                        FinishDate = (dto.FinishDate != null && dto.FinishDate != DateTime.MinValue) ? dto.FinishDate : DateTime.MaxValue,
                        PublishDate = (dto.FinishDate != null && dto.FinishDate != DateTime.MinValue) ? dto.FinishDate : DateTime.MinValue,
                        ImageUrl = dto.ImageUrl
                    };
                    await _context.News.AddAsync(news);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var news = await _context.News.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    news.CategoryId = dto.CategoryId;
                    news.Content = dto.Content;
                    news.Description = dto.Description;
                    news.FinishDate = (dto.FinishDate != null && dto.FinishDate != DateTime.MinValue) ? dto.FinishDate : DateTime.MaxValue;
                    news.ImageUrl = string.IsNullOrEmpty(dto.ImageUrl) ? news.ImageUrl : dto.ImageUrl;
                    news.IsActive = dto.IsActive;
                    news.PublishDate = (dto.FinishDate != null && dto.FinishDate != DateTime.MinValue) ? dto.FinishDate : DateTime.MinValue;
                    news.Title = dto.Title;
                    _context.News.Update(news);
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
                var news = await _context.News.FirstOrDefaultAsync(x => x.Id == id);
                _context.News.Remove(news);
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
