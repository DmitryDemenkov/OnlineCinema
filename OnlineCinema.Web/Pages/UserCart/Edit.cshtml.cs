using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages.UserCart
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public string CurrentType { get; set; }

        public FilmToOrder Film { get; set; }

        public Cart Cart { get; set; }

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("User"))
                return Redirect("/Login");

            if (Request.Cookies.TryGetValue<Cart>("Cart", out Cart cart))
                Cart = cart;
            else
                Cart = new Cart();

            if (!Cart.Films.Exists(x => x.Film.Id == Id))
                return Redirect("/UserCart/Index");

            Film = Cart.Films.Find(x => x.Film.Id == Id);
            CurrentType = Film.Type;

            return Page();
        }

        public IActionResult OnPostUpdate(int id)
        {
            Request.Cookies.TryGetValue<Cart>("Cart", out Cart cart);
            Cart = cart;

            if (CurrentType == "Аренда")
                Cart.UpdateType(id, Cart.PurchaseType.RENTAL);
            else
                Cart.UpdateType(id, Cart.PurchaseType.PURCHASE);

            Response.Cookies.Delete("Cart");
            Response.Cookies.Append<Cart>("Cart", Cart);

            return Redirect("/UserCart/Index");
        }

        public IActionResult OnPostDelete(int id)
        {
            Request.Cookies.TryGetValue<Cart>("Cart", out Cart cart);
            Cart = cart;

            Cart.Delete(id);

            Response.Cookies.Delete("Cart");
            Response.Cookies.Append<Cart>("Cart", Cart);

            return Redirect("/UserCart/Index");
        }
    }
}
