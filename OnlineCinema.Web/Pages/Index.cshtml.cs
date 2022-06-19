using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private DbFilmService filmService;

        public IEnumerable<Film> Films { get; private set; }

        [BindProperty (SupportsGet = true)]
        public string Title { get; set; }

        public IndexModel(ILogger<IndexModel> logger, DbFilmService dbFilmService)
        {
            _logger = logger;
            filmService = dbFilmService;
        }

        public IActionResult OnGet()
        {
            int errorCode;
            if (Title == null)
                Films = filmService.GetPopularFilms(out errorCode);
            else
                Films = filmService.GetFilmsByTitle(Title, out errorCode);
            
            if (errorCode != 0)
            {
                return Redirect($"/Error?DbError={errorCode}");
            }
            return Page();
        }
    }
}
