using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Extensions;
using OnlineCinema.Web.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace OnlineCinema.Web.Pages.UserCart
{
    public class IndexModel : PageModel
    { 
        public Cart Cart { get; set; }

        public User AuthorizedUser { get; set; }

        private DbOrderService OrderService;

        public IndexModel(DbOrderService orderService)
        {
            OrderService = orderService;
        }

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("User"))
                return Redirect("/Login");

            if (Request.Cookies.TryGetValue<Cart>("Cart", out Cart cart))
                Cart = cart;
            else
                Cart = new Cart();

            return Page();
        }

        public IActionResult OnPostEdit(int id)
        {
            return Redirect($"/UserCart/Edit?Id={id}");
        }

        public IActionResult OnPostAppend()
        {
            Request.Cookies.TryGetValue<User>("User", out User user);
            AuthorizedUser = user;

            Request.Cookies.TryGetValue<Cart>("Cart", out Cart cart);
            Cart = cart;

            OrderService.Append(Cart, AuthorizedUser, out int errorCode);

            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            Cart.Films.Clear();
            Response.Cookies.Delete("Cart");
            Response.Cookies.Append<Cart>("Cart", Cart);
            return Redirect("/Profile/History");
        }
    }
}
