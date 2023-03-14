using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSocialAPI.Application.Features.Commands;
using MovieSocialAPI.Application.Repositories;

namespace MovieSocialAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FavoritesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody]CreateFavoriteCommandRequest request)
        {
            CreateFavoriteCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFavorite([FromRoute] DeleteFavoriteCommandRequest request)
        {
            DeleteFavoriteCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
