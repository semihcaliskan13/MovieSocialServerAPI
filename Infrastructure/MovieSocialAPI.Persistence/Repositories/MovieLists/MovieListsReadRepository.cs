using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Application.Repositories.MovieLists;
using MovieSocialAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence.Repositories.MovieLists
{
    public class MovieListsReadRepository : ReadRepository<MovieSocialAPI.Domain.Entities.MovieList>, IMovieListsReadRepository
    {
        public MovieListsReadRepository(MovieSocialAPIDbContext context) : base(context)
        {
        }
    }
}
