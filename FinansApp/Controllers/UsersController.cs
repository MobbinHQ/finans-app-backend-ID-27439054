using FinansApp.Business.Users;
using FinansApp.Business.Users.Dto;
using FinansApp.Web.Models.Users;
using FinansApp.Web.ReturnObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("kullanicilar")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("kullanici-liste")]
        public async Task<JsonResult> GetList(int Start, int Length)
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var response = await _userService.GetFiltered(Start, Length, searchValue);
            //var restCategories = await _restCategoryService.GetList();
            var m = new UserViewModel();
            var l = new List<UModel>();
            foreach (var item in response)
            {
                var rm = new UModel()
                {
                    Email = item.Email,
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    IsActive = item.IsActive,
                    Phone = item.Phone
                };
                l.Add(rm);
            }
            m.UModels = l;
            var jsonData = new { recordsFiltered = await _userService.GetCountFiltered(searchValue), recordsTotal = m.UModels.Count, data = m.UModels };
            return Json(jsonData);
        }

        [HttpGet("kullanici-ekle")]
        [HttpGet("kullanici-duzenle/{id}")]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var dto = new UserAddOrEditDto();
            if (id.HasValue)
            {
                var res = await _userService.GetById(id.Value);
                //dto = _mapper.Map<UserAddOrEditDto>(res);
                dto.Id = res.Id;
                dto.Email = res.Email;
                dto.IsActive = res.IsActive;
                dto.Name = res.Name;
                //dto.Password = res.Password;
                dto.Surname = res.Surname;
                dto.UserType = res.UserType;
                dto.Phone = res.Phone;
            }
            //dto.Cities = _cityService.GetAll();
            return View(dto);
        }

        [HttpPost("kullanici-ekle")]
        [HttpPost("kullanici-duzenle/{id}")]
        public async Task<JsonResult> AddOrEdit(UserAddOrEditDto dto)
        {
            var response = new JsonReturnObject();
            //dto.CreatedBy = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            response.EC = await _userService.AddOrEdit(dto);
            return Json(response);
        }

        [HttpPost("kullanici-sil")]
        public async Task<bool> Delete(int id)
        {
            return await _userService.Delete(id);
        }
    }
}
