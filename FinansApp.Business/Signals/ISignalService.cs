using FinansApp.Business.Signals.Dto;
using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.Signals
{
    public interface ISignalService
    {
        Task<int> AddOrEdit(SignalAddOrEditDto dto);
        Task<int> Delete(int id);
        Task<Signal> Get(int id);
        Task<List<Signal>> GetAll();
        Task<List<Signal>> GetFiltered(int skip, int take, string search);
        Task<int> GetFilteredCount(string search);
    }
}
