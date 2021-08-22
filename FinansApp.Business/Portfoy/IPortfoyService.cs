using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinansApp.Business.Portfoy.Dto;

namespace FinansApp.Business.Portfoy
{
    public interface IPortfoyService
    {
        Task<bool> AddPortfoy(PortfoyAddDto dto, int userId);
        Task<bool> DeletePortfoy(int id);
        Task<List<Data.Tables.Portfoy>> GetListByUserId(int userId);
    }
}
