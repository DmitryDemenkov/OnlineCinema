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
    public class FilmModel : PageModel
    {
        [BindProperty(Name = "Id", SupportsGet = true)]
        public int idFilm { get; set; }

        public DbFilmService FilmService { get; set; }

        public Film Film { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Production> Productions { get; set; }

        public PurchaseState State { get; set; }

        public FilmModel(DbFilmService filmService)
        {
            FilmService = filmService;
        }

        public enum PurchaseState
        {
            NOUSER,
            LIBRARY,
            CART,
            PURCHASE
        }

        public IActionResult OnGet()
        {
            Film = FilmService.GetFilm(idFilm, out int errorCode);

            if (errorCode != 0)
                return Redirect($"Error?DbError={errorCode}");

            Genres = FilmService.GetGenres(idFilm, out errorCode);

            if (errorCode != 0)
                return Redirect($"Error?DbError={errorCode}");

            Productions = FilmService.GetProductions(Film, out errorCode);

            if (errorCode != 0)
                return Redirect($"Error?DbError={errorCode}");

            SetState();

            return Page();
        }

        public IActionResult OnPost()
        {
            Film = FilmService.GetFilm(idFilm, out int errorCode);

            if (errorCode != 0)
                return Redirect($"Error?DbError={errorCode}");

            if (!Request.Cookies.TryGetValue<Cart>("Cart" , out Cart cart))
                cart = new Cart();

            cart.Add(Film, Cart.PurchaseType.RENTAL);

            Response.Cookies.Delete("Cart");
            Response.Cookies.Append<Cart>("Cart", cart);

            return Redirect($"/UserCart/Edit?Id={idFilm}");
        }

        private void SetState()
        {
            if (!Request.Cookies.TryGetValue<User>("User", out User user))
            {
                State = PurchaseState.NOUSER;
            }
            else if (FilmService.HasInLibrary(Film, user))
            {
                State = PurchaseState.LIBRARY;
            }
            else if (Request.Cookies.TryGetValue<Cart>("Cart", out Cart cart))
            {
                if (cart.Contains(Film))
                    State = PurchaseState.CART;
                else
                    State = PurchaseState.PURCHASE;
            }
            else
            {
                State = PurchaseState.PURCHASE;
            }
        }
    }
}
