using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages.Profile
{
    public class HistoryModel : PageModel
    {
        public HistoryModel(DbUserService userService)
        {
            UserService = userService;
        }

        public IEnumerable<Order> Orders { get; private set; }

        private DbUserService UserService;

        public IActionResult OnGet()
        {
            if (Request.Cookies.TryGetValue<User>("User", out User user))
            {
                Orders = UserService.GetUsersHistory(user.Id, out int errorCode);

                if (errorCode != 0)
                    return RedirectToPage($"/Error?DbError={errorCode}");

                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }            
        }
    }
}
