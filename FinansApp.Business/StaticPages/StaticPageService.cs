using FinansApp.Business.StaticPages.Dto;
using FinansApp.Data;
using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.StaticPages
{
    public class StaticPageService : IStaticPageService
    {
        private readonly FinansAppDbContext _context;

        public StaticPageService(FinansAppDbContext context)
        {
            _context = context;
        }

        public async Task<StaticPage> GetById(int id)
        {
            return await _context.StaticPages.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<StaticPage>> GetFiltered(int skip, int take, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.StaticPages.Where(x => x.Name.Contains(search)).OrderBy(x => x.Name).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.StaticPages.OrderBy(x => x.Name).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetFilteredCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.StaticPages.Where(x => x.Name.Contains(search)).OrderBy(x => x.Name).CountAsync();
            }
            return await _context.StaticPages.OrderBy(x => x.Name).CountAsync();
        }
        public async Task<bool> EditText(int id, string text)
        {
            try
            {
                var sp = await _context.StaticPages.FirstOrDefaultAsync(x => x.PageId == id);
                if (sp != null)
                {
                    sp.Text = text;
                    _context.StaticPages.Update(sp);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var staticPage = new StaticPage
                    {
                        PageId = id,
                        Text = text
                    };
                    await _context.StaticPages.AddAsync(staticPage);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<int> AddOrEdit(StaticPageAddOrEditDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    var page = new StaticPage
                    {
                        Name = dto.Name,
                        Text = dto.Text,
                        Url = dto.Name.Replace(' ', '-').Replace('Ş', 's').Replace('ş', 's').Replace('ç', 'c').Replace('Ç', 'c').Replace('Ğ', 'g').Replace('ğ', 'g').Replace('I', 'i').Replace('ı', 'i').Replace('Ö', 'o').Replace('ö', 'o').Replace('Ü', 'u').Replace('ü', 'u').Replace('?', '-').Replace('&', '-').Replace('=', '-').ToString()
                    };
                    await _context.StaticPages.AddAsync(page);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var page = await _context.StaticPages.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    page.Name = dto.Name;
                    page.Text = dto.Text;
                    _context.StaticPages.Update(page);
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
                var pc = await _context.StaticPages.FirstOrDefaultAsync(x => x.Id == id);
                _context.StaticPages.Remove(pc);
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
