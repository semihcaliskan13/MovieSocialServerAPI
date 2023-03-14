using MediatR;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Queries.Movies
{
    public class GetMovieDetailsQueryRequest:IRequest<GetMovieDetailsQueryResponse>
    {
        public string Id { get; set; }
    }
    public class GetMovieDetailsQueryResponse
    {
        public Movie Movie { get; set; }
    }
    public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQueryRequest, GetMovieDetailsQueryResponse>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieDetailsQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<GetMovieDetailsQueryResponse> Handle(GetMovieDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            Movie movie =await _movieRepository.GetByIdAsync(request.Id);
            return new()
            {
                Movie = movie,
            };
            
        }
    }
}
