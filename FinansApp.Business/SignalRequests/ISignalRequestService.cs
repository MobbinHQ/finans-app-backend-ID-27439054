using FinansApp.Business.SignalRequests.Dto;
using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.SignalRequests
{
    public interface ISignalRequestService
    {
        Task<bool> Add(SignalRequestDto dto);
        Task<int> Delete(int id);
        Task<List<SignalRequest>> GetFiltered(int skip, int take, string search);
        Task<int> GetFilteredCount(string search);
    }
}
