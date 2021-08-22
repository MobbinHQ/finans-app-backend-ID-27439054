using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinansApp.Business.Portfoy.Dto;
using FinansApp.Data;
using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace FinansApp.Business.Portfoy
{
    public class PortfoyService : IPortfoyService
    {
        private readonly FinansAppDbContext _context;

        public PortfoyService(FinansAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FinansApp.Data.Tables.Portfoy>> GetListByUserId(int userId)
        {
            return await _context.Portfoys.OrderByDescending(x=>x.Date).Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> AddPortfoy(PortfoyAddDto dto,int userId)
        {
            try
            {
                var p = new FinansApp.Data.Tables.Portfoy
                {
                    Amount = dto.Amount,
                    CreateDate = DateTime.Now,
                    Price = dto.Price,
                    UserId = userId,
                    Code = dto.Code,
                    Date = dto.Date
                };
                await _context.Portfoys.AddAsync(p);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePortfoy(int id)
        {
            var p = await _context.Portfoys.FirstOrDefaultAsync(x => x.Id == id);
            _context.Portfoys.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
