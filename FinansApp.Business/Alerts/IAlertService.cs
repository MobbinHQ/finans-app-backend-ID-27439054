using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinansApp.Business.Alerts.Dto;
using FinansApp.Data.Tables;

namespace FinansApp.Business.Alerts
{
    public interface IAlertService
    {
        Task<int> Add(AddAlertDto dto, int userId);
        Task<bool> DeleteAlert(int id);
        Task<List<Alert>> GetListByUserId(int userId);
    }
}
