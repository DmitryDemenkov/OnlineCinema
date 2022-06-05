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
    public class LibraryModel : PageModel
    {
        public LibraryModel(DbUserService userService)
        {
            UserService = userService;
        }

        public IEnumerable<FilmToLibrary> Films { get; private set; }

        private DbUserService UserService;

        public IActionResult OnGet()
        {
            if (Request.Cookies.TryGetValue<User>("User", out User user))
            {
                Films = UserService.GetUserLibrary(user, out int errorCode);

                if (errorCode != 0)
                    return Redirect($"/Error?DbError={errorCode}");

                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
