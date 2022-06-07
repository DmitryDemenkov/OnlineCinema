using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.Pages
{
    public class GenreModel : PageModel
    {
        public GenreModel(DbGenreService genreService)
        {
            GenreService = genreService;
        }

        [BindProperty(Name = "Id", SupportsGet = true)]
        public int GenreId { get; set; }

        [BindProperty(Name = "Name", SupportsGet = true)]
        public string GenreName { get; set; } = "Жанр";

        public IEnumerable<Film> Films { get; set; }

        private DbGenreService GenreService;

        public IActionResult OnGet()
        {
            Films = GenreService.GetFilms(GenreId, out int errorCode);

            if (errorCode != 0)
                return Redirect($"Error?DbError={errorCode}");

            return Page();
        }
    }
}
