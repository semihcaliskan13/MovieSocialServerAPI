using MediatR;
using MovieSocialAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Commands
{
    public class DeleteFavoriteCommandRequest : IRequest<DeleteFavoriteCommandResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommandRequest, DeleteFavoriteCommandResponse>
    {
        private readonly IFavoriteReadRepository _favoriteReadRepository;
        private readonly IFavoriteWriteRepository _favoriteWriteRepository;

        public DeleteFavoriteCommandHandler(IFavoriteReadRepository favoriteReadRepository, IFavoriteWriteRepository favoriteWriteRepository)
        {
            _favoriteReadRepository = favoriteReadRepository;
            _favoriteWriteRepository = favoriteWriteRepository;
        }


        public async Task<DeleteFavoriteCommandResponse> Handle(DeleteFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            await _favoriteWriteRepository.RemoveAsync(request.Id.ToString());
            await _favoriteWriteRepository.SaveAsync();


            return new();
        }
    }
    public class DeleteFavoriteCommandResponse
    {
        public string Message { get; set; }
    }
}
