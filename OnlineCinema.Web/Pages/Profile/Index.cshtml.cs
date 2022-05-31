using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineCinema.Web.Pages.Profile
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("User"))
                return RedirectToPage("/Index");

            return Page();
        }
    }
}
