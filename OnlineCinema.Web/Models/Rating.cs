using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.Models
{
    public class Rating
    {
        public Rating(string userName, float action, float actorPlay, float plot, float effects, float middle)
        {
            UserName = userName;
            Action = action;
            ActorPlay = actorPlay;
            Plot = plot;
            Effects = effects;
            Middle = middle;
        }

        public Rating(Film film, float action, float actorPlay, float plot, float effects, float middle)
        {
            Film = film;
            Action = action;
            ActorPlay = actorPlay;
            Plot = plot;
            Effects = effects;
            Middle = middle;
        }

        public string UserName { get; private set; }

        public Film Film { get; private set; }

        public float Action { get; private set; }

        public float ActorPlay { get; private set; }

        public float Plot { get; private set; }

        public float Effects { get; private set; }

        public float Middle { get; private set; }
    }
}
