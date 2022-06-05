using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages.Profile
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public DateTime BirthDate { get; set; } = default;

        [BindProperty]
        public long UserId { get; set; }

        public string Message { get; private set; } = "";

        private DbUserService userService;

        public EditModel(DbUserService dbUserService)
        {
            userService = dbUserService;
        }
        
        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("User"))
                return RedirectToPage("/Index");

            Request.Cookies.TryGetValue<User>("User", out User user);
            SetFields(user);
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
                User updatedUser = new User(Login, Password, Email, BirthDate, UserId);
                User user = userService.UpdateUser(updatedUser, out int errorCode);

                if (HasDublicateData(errorCode))
                    return Page();

                else if (errorCode != 0)
                    return Redirect($"/Error?DbError={errorCode}");

                else
                {
                    SetFields(user);
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

        private void SetFields(User user)
        {
            UserId = user.Id;
            Login = user.Login;
            Password = user.Password;
            Email = user.Email;
            BirthDate = user.BirthDate;
        }
    }
}
