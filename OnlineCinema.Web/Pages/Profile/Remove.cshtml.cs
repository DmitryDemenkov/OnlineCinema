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
    public class RemoveModel : PageModel
    {
        public RemoveModel(DbUserService dbUserService)
        {
            UserService = dbUserService;
        }

        private DbUserService UserService;

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("User"))
                return RedirectToPage("/Index");

            return Page();
        }

        public IActionResult OnPostYes()
        {
            Request.Cookies.TryGetValue<User>("User", out User user);
            
            UserService.RemoveUser(user, out int errorCode);
            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            Response.Cookies.Delete("User");
            Response.Cookies.Delete("Cart");
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostNo()
        {
            return RedirectToPage("./Index");
        }
    }
}
