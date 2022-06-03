using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages
{
    public class SignupModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public DateTime BirthDate { get; set; } = default;

        public string Message { get; private set; } = "";

        private DbUserService userService;

        public SignupModel(DbUserService dbUserService)
        {
            userService = dbUserService;
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies.ContainsKey("User"))
            {
                return RedirectToPage("/Profile/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Login == null || Password == null || Email == null)
            {
                Message = "Необходимо ввести логин, пароль и адрес электронной почты";
                return Page();
            }
            else
            {
                User user = userService.AddNewUser(Login, Password, Email, out int errorCode, BirthDate);
                
                if (HasDublicateData(errorCode))
                    return Page();
                
                else if (errorCode != 0)
                    return Redirect($"/Error?DbError={errorCode}");
                
                else
                {
                    Response.Cookies.Append<User>("User", user);
                    return RedirectToPage("/Profile/Index");
                }
            }
        }


        private bool HasDublicateData(int errorCode)
        {
            if (errorCode == -1)
            {
                Message = "Пользователь с таким логином уже существует";
                return true;
            }
            else if (errorCode == -2)
            {
                Message = "Пользователь с таким адресом электронной почты уже существует";
                return true;
            }
            return false;
        }
    }
}
