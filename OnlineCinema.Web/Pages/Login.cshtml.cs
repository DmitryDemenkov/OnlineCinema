using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; private set; } = "";

        private DbUserService userService;

        public LoginModel(DbUserService dbUserService)
        {
            userService = dbUserService;
        }

        public ActionResult OnGet()
        {
            if (Request.Cookies.ContainsKey("IdentifiedUser"))
            {
                return RedirectToPage("/User");
            }
            return Page();
        }

        public ActionResult OnPost()
        {
            if (Login == null || Password == null)
            {
                Message = "Необходимо ввести логин и пароль";
                return Page();
            }
            else
            {
                User user = userService.GetByLogin(Login, Password, out int errorCode);
                if (user == null && errorCode == 0)
                {
                    Message = "Неверно указан логин или пароль";
                    return Page();
                }
                else if (errorCode != 0)
                {
                    return Redirect($"/Error?DbError={errorCode}");
                }
                else
                {
                    Response.Cookies.Append<User>("IdentifiedUser", user);
                    return RedirectToPage("/User");
                }
            }
        }
    }
}
