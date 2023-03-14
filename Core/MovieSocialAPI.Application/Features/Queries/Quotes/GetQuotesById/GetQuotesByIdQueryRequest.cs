using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Queries.GetQuotesById
{
    public class GetQuotesByIdQueryRequest : IRequest<GetQuotesByIdQueryResponse>
    {
        public string Id { get; set; }
    }
    public class GetQuotesByIdQueryResponse
    {
        public Quote? Quote { get; set; }
    }
    public class GetQuotesByIdQueryHandler : IRequestHandler<GetQuotesByIdQueryRequest, GetQuotesByIdQueryResponse>
    {
        readonly private IQuoteReadRepository _readRepository;
        readonly private IMovieRepository _movieRepository;

        public GetQuotesByIdQueryHandler(IQuoteReadRepository readRepository, IMovieRepository movieRepository)
        {
            _readRepository = readRepository;
            _movieRepository = movieRepository;
        }

        public async Task<GetQuotesByIdQueryResponse> Handle(GetQuotesByIdQueryRequest request, CancellationToken cancellationToken)
        {

            
           Quote quote = await _readRepository.GetByIdAsync(request.Id, false);
            quote.Movie = await _movieRepository.GetByIdAsync(quote.MovieId);
            return new()
            {
                Quote = quote
            };
        }
    }
}
