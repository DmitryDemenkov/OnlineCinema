using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Services;

namespace OnlineCinema.Web.Pages
{
    public class FilmModel : PageModel
    {
        public DbFilmService FilmService { get; set; }

        public Film Film { get; set; }

        [BindProperty(Name = "Id", SupportsGet = true)]
        public int idFilm { get; set; }

        public FilmModel(DbFilmService filmService)
        {
            FilmService = filmService;
        }

        public void OnGet()
        {
            Film = FilmService.GetFilm(idFilm, out int errorCode);
        }
    }
}
