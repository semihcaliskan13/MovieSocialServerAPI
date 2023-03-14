using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSocialAPI.Application.Features.Queries.Movies;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;

namespace MovieSocialAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediatR;
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMediator mediatR, IMovieRepository movieRepository)
        {
            _mediatR = mediatR;
            _movieRepository = movieRepository;
        }

        [HttpGet("{Id}")]
        public  async Task<IActionResult> GetMovieDetail([FromRoute]GetMovieDetailsQueryRequest getMovieDetailsQueryRequest)
        {
            GetMovieDetailsQueryResponse getMovieDetailsQueryResponse = await _mediatR.Send(getMovieDetailsQueryRequest);
            return Ok(getMovieDetailsQueryResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecommended()
        {
            List<Movie> movies=await _movieRepository.GetRecommended();  
            return Ok(movies);
        }
    }
}
