using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using MovieSocialAPI.Domain.Entities.Identity;

namespace MovieSocialAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        readonly IFollowerReadRepository _followerReadRepository;
        readonly IFollowerWriteRepository _followerWriteRepository;
        readonly IFollowingReadRepository _followingReadRepository;
        readonly IFollowingWriteRepository _followingWriteRepository;

        public FollowsController(IFollowerReadRepository followerReadRepository, IFollowerWriteRepository followerWriteRepository, IFollowingReadRepository followingReadRepository, IFollowingWriteRepository followingWriteRepository)
        {
            _followerReadRepository = followerReadRepository;
            _followerWriteRepository = followerWriteRepository;
            _followingReadRepository = followingReadRepository;
            _followingWriteRepository = followingWriteRepository;
        }




        [HttpPost]
        public async Task<IActionResult> FollowUser([FromBody] Following following)
        {
            Follower follower = new Follower()
            {
                Id = following.Id,
                UserId = following.FollowingsId,
                FollowersId = following.UserId,

            };
            bool nedir = await _followingWriteRepository.AddAsync(following);
            await _followingWriteRepository.SaveAsync();

            bool ikinci = await _followerWriteRepository.AddAsync(follower);
            int deneme = await _followerWriteRepository.SaveAsync();


            return Ok();

        }

        [HttpDelete("{followerId}")]
        public async Task<IActionResult> Unfollow([FromRoute] string followerId)
        {

            await _followerWriteRepository.RemoveAsync(followerId);
            await _followerWriteRepository.SaveAsync();

            await _followingWriteRepository.RemoveAsync(followerId);
            await _followingWriteRepository.SaveAsync();    
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetFollowers([FromRoute]string id)
        {
           var followers = _followerReadRepository.GetWhere(userId=>userId.UserId==int.Parse(id)).Select(data => new { 

               Id = data.Id,
               UserId = data.UserId,
               FollowersId = data.FollowersId,
               
            });
            return Ok(followers);
        }

    }
}
