using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieSocialAPI.Application.Abstractions.Token;
using MovieSocialAPI.Application.DTOS;
using MovieSocialAPI.Application.Exceptions;
using MovieSocialAPI.Application.Repositories.UserDetail;
using MovieSocialAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public string Message { get; set; }
        public MovieSocialAPI.Domain.Entities.Identity.AppUser User { get; set; }
        public UserEntity UserInfo { get; set; }

    }
    public class UserEntity
    {
        public int UserDetailId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string  SurName { get; set; }
    }
    public class LoginUserCommangHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        
        
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserDetailReadRepository _userDetailReadRepository;


        public LoginUserCommangHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler, IUserDetailReadRepository userDetailReadRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userDetailReadRepository = userDetailReadRepository;
        }


        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);            
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            

            if (user == null)
            {
                throw new UserNotFoundException();
            }//buradaki false şifre yanlış girilirse adamı kilitleyelim mi?
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            UserEntity UserInfo = new UserEntity { Name=user.Name,UserDetailId=user.UserDetailId,UserEmail=user.Email,SurName=user.Surname,Id=user.Id,UserName=user.UserName};
            
            if (signInResult.Succeeded)
            {
                Token token = _tokenHandler.CreateAccess(5, user.Id.ToString());
                
                return new()
                {
                    UserInfo=UserInfo,
                    Token = token,
                    Message = "Giriş Başarılı"
                };

            }
            return new()
            {
                Message = "Kullanıcı Adı Veya Şifre Yanlış!"//burada dakika 55 gibi hata fırlatılabilir.

                
        };

        }
    }
}
