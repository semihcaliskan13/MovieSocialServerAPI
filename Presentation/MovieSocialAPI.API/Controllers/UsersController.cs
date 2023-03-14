using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieSocialAPI.Application.Features.Commands.AppUser.CreateUser;
using MovieSocialAPI.Application.Features.Commands.AppUser.LoginUser;
using MovieSocialAPI.Domain.Entities.Identity;
using System.Net;

namespace MovieSocialAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        

        public UsersController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        [HttpPost]
        //? Kullanıcı ekleme metodu.
        //! form data değil, direkt json objesini parametre olarak alıyor.
        
        
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            if (response.Succeeded)
            {
                return Created("",response);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.NotAcceptable,response);
            }
            
        }
        //!Kullanıcı login etme metodu daha sonra bir de jwt cookie döndürüyor.
        //todo: jwt cookie set ettik, bir de onu sign out ile silmeyi öğreneceğiz.
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            if (response.Message== "Giriş Başarılı")
            {
                Response.Cookies.Append(
                    "access_token",
             response.Token.AccesssToken,
            new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true,


            });
                return Ok(response);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.NotAcceptable, response);
            }
            
            
        }
        // çerezi silerek signOut tarzı bir şey yapmış oluyoruz.
        [HttpGet("[action]")]
        public async Task<IActionResult> SignOut()
        {
            Response.Cookies.Delete("access_token");
            return Ok();
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetUserInfo([FromRoute] string userId)
        {
            AppUser userInfo = await _userManager.FindByIdAsync(userId);
            return Ok(userInfo);
        }
    }
}
