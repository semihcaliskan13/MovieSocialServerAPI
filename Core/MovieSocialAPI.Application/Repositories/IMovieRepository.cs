using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Repositories
{
    public interface IMovieRepository
    {
        
        IQueryable<Movie> GetAll();
        Task<Movie> GetByIdAsync(string id);

        Task<List<Movie>> GetRecommended();
    }
}
