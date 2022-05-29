﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineCinema.Web.Repositories;
using OnlineCinema.Web.RepositoryInterfaces;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.Services
{
    public class DbFilmService
    {
        public DbFilmService()
        {
            filmRepository = new MySqlDbFilmRepository();
        }

        private IFilmRepository filmRepository;

        public IEnumerable<Film> GetPopularFilms(out int errorCode)
        {
            try
            {
                IEnumerable<Film> films = filmRepository.GetPopular();
                errorCode = 0;
                return films;
            }
            catch (OnlineCinemaException exception)
            {
                errorCode = exception.ErrorCode;
                return null;
            }
        }
    }
}