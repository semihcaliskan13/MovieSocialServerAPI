using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSocialAPI.Application.Abstractions.Storage;
using MovieSocialAPI.Application.Repositories.UserDetail;
using MovieSocialAPI.Domain.Entities;
using MovieSocialAPI.Domain.ViewModels;
using System.IO;

namespace MovieSocialAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {

        readonly IStorageService _storageService;
        readonly IUserDetailReadRepository _userDetailReadRepository;
        readonly IUserDetailWriteRepository _userDetailWriteRepository;

        public UserDetailsController(IStorageService storageService, IUserDetailReadRepository userDetailReadRepository, IUserDetailWriteRepository userDetailWriteRepository)
        {
            _storageService = storageService;
            _userDetailReadRepository = userDetailReadRepository;
            _userDetailWriteRepository = userDetailWriteRepository;
        }

        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> GetProfilePhoto([FromRoute] string userName, [FromQuery] string Id)
        {
            var imagePath=await _userDetailReadRepository.GetByIdAsync(Id);
            
            var datas = _storageService.GetFiles(userName);
            var lastData = "";
            var contain= imagePath.ProfileImage.Substring(imagePath.ProfileImage.IndexOf('/')+1);
            foreach (var data in datas)
            {
                if (data.Contains(contain))//todo: startswith mantıklı.                   
                {
                    lastData = data;
                }

            }

            return Ok(lastData);

        }
        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> GetWallPaperPhoto([FromRoute] string userName, [FromQuery] string Id)
        {
            var imagePath = await _userDetailReadRepository.GetByIdAsync(Id);

            var datas = _storageService.GetFiles(userName+"wallpaper");
            var lastData = "";
            var contain = imagePath.WallPaper.Substring(imagePath.WallPaper.IndexOf('/') + 1);
            foreach (var data in datas)
            {
                if (data.Contains(contain))//todo: startswith mantıklı.                   
                {
                    lastData = data;
                }

            }

            return Ok(lastData);

        }
        [HttpPut("[action]/{userName}")]
        public async Task<IActionResult> UploadPhoto([FromRoute] string userName, [FromQuery] string id, [FromQuery] PutUserDescription putUserDescription)
        {
            string path = "";
            List<(string fileName, string pathOContainerName)> result = await _storageService.UploadAsync(userName, Request.Form.Files);
            foreach (var item in result)
            {
                path = item.pathOContainerName;
            }
            _userDetailWriteRepository.Update(new UserDetail
            {
                Id = int.Parse(id),
                Description = putUserDescription.Description,
                CreatedTime = putUserDescription.CreatedTime,
                UpdatedTime = putUserDescription.UpdatedTime,
                WallPaper = putUserDescription.WallPaper,
                ProfileImage = path,

            });

            await _userDetailWriteRepository.SaveAsync();

            return Ok();
        }
        [HttpPut("[action]/{userName}")]
        public async Task<IActionResult> UploadWallpaper([FromRoute] string userName, [FromQuery] string id, [FromQuery] PutUserDescription putUserDescription)
        {
            string path = "";
            List<(string fileName, string pathOContainerName)> result = await _storageService.UploadAsync(userName+"wallpaper", Request.Form.Files);
            foreach (var item in result)
            {
                path = item.pathOContainerName;
            }
            _userDetailWriteRepository.Update(new UserDetail
            {
                Id = int.Parse(id),
                Description = putUserDescription.Description,
                CreatedTime = putUserDescription.CreatedTime,
                UpdatedTime = putUserDescription.UpdatedTime,
                WallPaper = path,
                ProfileImage = putUserDescription.ProfileImage,


            });

            await _userDetailWriteRepository.SaveAsync();

            return Ok();
        }


        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> Description([FromRoute] string Id, [FromBody] PutUserDescription putUserDescription)
        {
            _userDetailWriteRepository.Update(new UserDetail
            {
                Id = int.Parse(Id),
                Description = putUserDescription.Description,
                CreatedTime= putUserDescription.CreatedTime,
                UpdatedTime= putUserDescription.UpdatedTime,    
                WallPaper= putUserDescription.WallPaper,    
                ProfileImage= putUserDescription.ProfileImage,                          
            });


            await _userDetailWriteRepository.SaveAsync();


            return Ok();
        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetDescription([FromRoute] string Id)
        {
            var desc = await _userDetailReadRepository.GetByIdAsync(Id);
            return Ok(desc);
        }

        

    }
}
