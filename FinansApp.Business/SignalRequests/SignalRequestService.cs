using FinansApp.Business.SignalRequests.Dto;
using FinansApp.Data;
using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.SignalRequests
{
    public class SignalRequestService : ISignalRequestService
    {
        private readonly FinansAppDbContext _context;

        public SignalRequestService(FinansAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SignalRequest>> GetFiltered(int skip, int take, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.SignalRequests.Where(x => x.NameSurname.Contains(search) || x.Email.Contains(search)).OrderBy(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.SignalRequests.OrderBy(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> GetFilteredCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.SignalRequests.Where(x => x.NameSurname.Contains(search) || x.Email.Contains(search)).CountAsync();
            }
            return await _context.SignalRequests.CountAsync();
        }

        public async Task<bool> Add(SignalRequestDto dto)
        {
            try
            {
                var signal = new SignalRequest
                {
                    Email = dto.Email,
                    NameSurname = dto.NameSurname,
                    PhoneNumber = dto.PhoneNumber,
                    CreateDate = DateTime.Now
                };
                await _context.SignalRequests.AddAsync(signal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var signal = await _context.SignalRequests.FirstOrDefaultAsync(x => x.Id == id);
                _context.SignalRequests.Remove(signal);
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
