using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinansApp.Business.Alerts.Dto;
using FinansApp.Data;
using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace FinansApp.Business.Alerts
{
    public class AlertService : IAlertService
    {
        private readonly FinansAppDbContext _context;

        public AlertService(FinansAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Alert>> GetListByUserId(int userId)
        {
            return await _context.Alerts.Where(x => x.UserId == userId && x.IsActive).ToListAsync();
        }

        public async Task<bool> DeleteAlert(int id)
        {
            try
            {
                var alert = await _context.Alerts.FirstOrDefaultAsync(x => x.Id == id);
                _context.Alerts.Remove(alert);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> Add(AddAlertDto dto,int userId)
        {
            try
            {
                var alert = new Alert
                {
                    UserId = userId,
                    AlertType = dto.AlertType,
                    BuySell = dto.BuySell,
                    Limit = dto.Limit
                };
                await _context.Alerts.AddAsync(alert);
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
