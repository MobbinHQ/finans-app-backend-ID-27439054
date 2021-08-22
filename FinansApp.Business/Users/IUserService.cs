using FinansApp.Business.Users.Dto;
using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Business.Users
{
    public interface IUserService
    {
        Task<int> AddOrEdit(UserAddOrEditDto dto);
        Task<bool> Delete(int id);
        Task<User> EmailControl(string email);
        Task<User> GetByEmail(string Email);
        Task<User> GetById(int id);
        Task<int> GetCountFiltered(string search);
        Task<List<User>> GetFiltered(int skip, int take, string search);
        Task<User> Login(LoginDto dto);
        Task<User> RegisterMobile(RegisterDto dto);
    }
}
