using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages.Ratings
{
    public class IndexModel : PageModel
    {
        public IndexModel(DbRatingService dbRatingService, DbFilmService dbFilmService)
        {
            RatingService = dbRatingService;
            FilmService = dbFilmService;
        }

        [BindProperty (Name = "Id", SupportsGet = true)]
        public int IdFilm { get; set; }

        private DbRatingService RatingService;

        private DbFilmService FilmService;

        public IEnumerable<Rating> Ratings { get; set; }

        public Film Film { get; set; }

        public IActionResult OnGet()
        {
            Film = FilmService.GetFilm(IdFilm, out int errorCode);

            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            if (Film == null)
                return Redirect("/Index");

            Request.Cookies.TryGetValue<User>("User", out User user);

            Ratings = RatingService.GetByFilm(Film, out errorCode, user);

            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            return Page();
        }
    }
}
