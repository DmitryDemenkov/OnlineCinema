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
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int IdFilm { get; set; }

        [BindProperty]
        public float Action { get; set; }

        [BindProperty]
        public float ActorPlay { get; set; }

        [BindProperty]
        public float Effects { get; set; }

        [BindProperty]
        public float Plot { get; set; }

        [BindProperty]
        public ConfirmType Type { get; set; } = ConfirmType.UPDATE;

        public User AutorizedUser { get; set; }

        public Film Film { get; set; }

        public Rating Rating { get; set; }

        public enum ConfirmType
        {
            APPEND,
            UPDATE
        }

        private DbFilmService FilmService;

        private DbRatingService RatingService { get; set; }

        public EditModel(DbFilmService dbFilmService, DbRatingService dbRatingService)
        {
            FilmService = dbFilmService;
            RatingService = dbRatingService;
        }
        
        public IActionResult OnGet()
        {
            Film = FilmService.GetFilm(IdFilm, out int errorCode);
            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            if (!Request.Cookies.TryGetValue<User>("User", out User user))
                return Redirect($"/Ratings/Index?Id={IdFilm}");

            AutorizedUser = user;

            Rating = RatingService.GetRating(Film, AutorizedUser, out errorCode);
            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            if (Rating == null)
            {
                Rating = new Rating(AutorizedUser, 5, 5, 5, 5, 5);
                Type = ConfirmType.APPEND;
            }

            Action = Rating.Action;
            ActorPlay = Rating.ActorPlay;
            Plot = Rating.Plot;
            Effects = Rating.Effects;

            return Page();
        }

        public IActionResult OnPostConfirm()
        {
            if (!Request.Cookies.TryGetValue<User>("User", out User user))
                return Redirect($"/Ratings/Index?Id={IdFilm}");

            float middleRating = (Action + ActorPlay + Effects + Plot) / 4;
            Rating = new Rating(user, Action, ActorPlay, Plot, Effects, middleRating);

            int errorCode;
            if (Type == ConfirmType.APPEND)
                RatingService.AppendRating(IdFilm, Rating, out errorCode);
            else
                RatingService.UpdateRating(IdFilm, Rating, out errorCode);

            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            return Redirect($"/Ratings?Id={IdFilm}");
        }

        public IActionResult OnPostDelete()
        {
            if (!Request.Cookies.TryGetValue<User>("User", out User user))
                return Redirect($"/Ratings/Index?Id={IdFilm}");

            RatingService.DeleteRating(user.Id, IdFilm, out int errorCode);
            if (errorCode != 0)
                return Redirect($"/Error?DbError={errorCode}");

            return Redirect($"/Ratings?Id={IdFilm}");
        }
    }
}
