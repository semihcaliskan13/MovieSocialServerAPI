using MediatR;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Commands
{
    public class CreateFavoriteCommandRequest:IRequest<CreateFavoriteCommandResponse>
    {
        
        public int UserId { get; set; }
        public int QuoteId { get; set; }
    }
    public class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommandRequest, CreateFavoriteCommandResponse>
    {
        private readonly IFavoriteReadRepository _favoriteReadRepository;
        private readonly IFavoriteWriteRepository _favoriteWriteRepository;

        public CreateFavoriteCommandHandler(IFavoriteReadRepository favoriteReadRepository, IFavoriteWriteRepository favoriteWriteRepository)
        {
            _favoriteReadRepository = favoriteReadRepository;
            _favoriteWriteRepository = favoriteWriteRepository;
        }
        public async Task<CreateFavoriteCommandResponse> Handle(CreateFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            Favorite favorite = new Favorite
            {
               
                UserId = request.UserId,
                QuoteId = request.QuoteId,
            };
           await _favoriteWriteRepository.AddAsync(favorite);
           await _favoriteWriteRepository.SaveAsync();
           int id = favorite.Id;

            return new(){ DeleteId = id, Message = "Oldu." };
        }
    }
    public class CreateFavoriteCommandResponse
    {
        public string Message { get; set; }
        public int DeleteId { get; set; }
    }
}
