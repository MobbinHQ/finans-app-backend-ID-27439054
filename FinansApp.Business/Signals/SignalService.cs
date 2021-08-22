using FinansApp.Business.Signals.Dto;
using FinansApp.Data;
using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.Signals
{
    public class SignalService : ISignalService
    {
        private readonly FinansAppDbContext _context;

        public SignalService(FinansAppDbContext context)
        {
            _context = context;
        }

        public async Task<Signal> Get(int id)
        {
            return await _context.Signals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Signal>> GetAll()
        {
            return await _context.Signals.ToListAsync();
        }

        public async Task<List<Signal>> GetFiltered(int skip, int take, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.Signals.Where(x => x.BuySell.Contains(search)).OrderBy(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.Signals.OrderBy(x => x.CreateDate).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> GetFilteredCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.Signals.Where(x => x.BuySell.Contains(search)).CountAsync();
            }
            return await _context.Signals.CountAsync();
        }
        public async Task<int> AddOrEdit(SignalAddOrEditDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    var signal = new Signal
                    {
                        BuySell = dto.BuySell,
                        CreateDate = DateTime.Now,
                        Entry = dto.Entry,
                        SL = dto.SL,
                        TP = dto.TP,
                        Type = dto.Type
                    };
                    await _context.Signals.AddAsync(signal);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var signal = await _context.Signals.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    signal.BuySell = dto.BuySell;
                    signal.Type = dto.Type;
                    signal.Entry = dto.Entry;
                    signal.SL = dto.SL;
                    signal.TP = dto.TP;
                    _context.Signals.Update(signal);
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
                var signal = await _context.Signals.FirstOrDefaultAsync(x => x.Id == id);
                _context.Signals.Remove(signal);
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
