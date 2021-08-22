using Microsoft.EntityFrameworkCore;
using FinansApp.Business.Users.Dto;
using FinansApp.Data;
using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinansApp.Business.UsefulFunctions;

namespace FinansApp.Business.Users
{
    public class UserService : IUserService
    {
        private readonly FinansAppDbContext _context;
        //private readonly AppSettings _appSettings;
        public UserService(FinansAppDbContext context)
        {
            _context = context;
        }
        #region Both
        public async Task<User> EmailControl(string email) => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        #endregion
        #region Web
        /// <summary>
        /// Datatable dolduran servis.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<List<User>> GetFiltered(int skip, int take, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.Users.Where(x => x.Name.Contains(search) || x.Surname.Contains(search) || x.Email.Contains(search)).OrderByDescending(x => x.Name).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.Users.OrderByDescending(x => x.Name).Skip(skip).Take(take).ToListAsync();
        }
        /// <summary>
        /// Datatable arama kelimesine göre mekan sayısını döner.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<int> GetCountFiltered(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.Users.Where(x => x.Name.Contains(search) || x.Surname.Contains(search) || x.Email.Contains(search)).CountAsync();
            }
            return await _context.Users.CountAsync();
        }
        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User> GetByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
        }
        public async Task<User> Login(LoginDto dto)
        {
            var tt = Crypt.CreateMD5(Crypt.CreateMD5(dto.Password));
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email && x.Password == Crypt.CreateMD5(Crypt.CreateMD5(dto.Password)));
        }

        public async Task<int> AddOrEdit(UserAddOrEditDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    var u = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
                    if (u != null)
                    {
                        return 1;
                    }
                    var user = new User
                    {
                        Email = dto.Email,
                        CreateDate = DateTime.Now,
                        IsActive = dto.IsActive,
                        Name = dto.Name,
                        Surname = dto.Surname,
                        Password = Crypt.CreateMD5(Crypt.CreateMD5(dto.Password)),
                        Phone = dto.Phone,
                        UserType = dto.UserType
                    };
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return 0;
                }
                else
                {
                    var u = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    if (u.Email != dto.Email)
                    {
                        var res = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
                        if (res != null)
                            return 1;
                    }
                    u.Email = dto.Email;
                    u.IsActive = dto.IsActive;
                    u.Name = dto.Name;
                    u.Phone = dto.Phone;
                    u.UserType = dto.UserType;
                    u.Password = !string.IsNullOrEmpty(dto.Password) ? Crypt.CreateMD5(Crypt.CreateMD5(dto.Password)) : u.Password;
                    u.Surname = dto.Surname;
                    _context.Users.Update(u);
                    await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Mobile
        public async Task<User> RegisterMobile(RegisterDto dto)
        {
            try
            {
                var user = new User
                {
                    CreateDate = DateTime.Now,
                    Email = dto.Email,
                    IsActive = true,
                    Name = dto.Name,
                    UserType = 2,
                    Password = Crypt.CreateMD5(Crypt.CreateMD5(dto.Password)),
                    Surname = dto.Surname
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
