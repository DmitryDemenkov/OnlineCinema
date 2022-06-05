using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages.Profile
{
    public class IndexModel : PageModel
    {
        public User IdentifiedUser { get; private set; }

        public IActionResult OnGet()
        {
            if (Request.Cookies.TryGetValue<User>("User", out User user))
            {
                IdentifiedUser = user;
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
