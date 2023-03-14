using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSocialAPI.Application.Repositories.MovieLists;
using MovieSocialAPI.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace MovieSocialAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieListsController : ControllerBase
    {
        readonly private IMovieListsReadRepository _movieListsReadRepository;
        readonly private IMovieListsWriteRepository _movieListsWriteRepository;

        public MovieListsController(IMovieListsReadRepository movieListsReadRepository, IMovieListsWriteRepository movieListsWriteRepository)
        {
            _movieListsReadRepository = movieListsReadRepository;
            _movieListsWriteRepository = movieListsWriteRepository;
        }

        [HttpGet("{UserId}")]
        public  IActionResult GetUsersMovieLists(string UserId)
        {
            MovieList? movieList = _movieListsReadRepository.Table.Include(data => data.MovieListMovies).FirstOrDefault(p => p.AppUserId == int.Parse(UserId));
            if (movieList == null) { }
            
            return Ok(movieList);

        }
        [HttpPost("{AppUserId}")]
        public async Task<IActionResult> AddList([FromQuery]string movieListName, [FromRoute] string AppUserId, [FromBody] List<MovieListMovie> movie)
        {
            
            await _movieListsWriteRepository.AddAsync(new()
            {
                MovieListName = movieListName,
                AppUserId = int.Parse(AppUserId),
                MovieListMovies= movie
                
            });
            await _movieListsWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
