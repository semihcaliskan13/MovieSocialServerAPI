using MediatR;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Queries.Quotes.GetQuotesByMovieId
{
    public class GetQuotesByMovieIdQueryRequest : IRequest<GetQuotesByMovieIdQueryResponse>
    {
        public string MovieId { get; set; }
    }
    public class GetQuotesByMovieIdQueryResponse
    {
        public IQueryable<Quote>? Quotes { get; set; }
    }
    public class GetQuotesByMovieQueryHandler : IRequestHandler<GetQuotesByMovieIdQueryRequest, GetQuotesByMovieIdQueryResponse>
    {
        readonly private IQuoteReadRepository _quoteReadRepository;
        readonly private IMovieRepository _movieRepository;

        public GetQuotesByMovieQueryHandler(IMovieRepository movieRepository, IQuoteReadRepository quoteReadRepository)
        {
            _movieRepository = movieRepository;
            _quoteReadRepository = quoteReadRepository;
        }

        public async Task<GetQuotesByMovieIdQueryResponse> Handle(GetQuotesByMovieIdQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Quote> quotes = _quoteReadRepository.GetWhere(data => data.MovieId == request.MovieId);

            foreach (var quote in quotes)
            {
                quote.Movie = await _movieRepository.GetByIdAsync(quote.MovieId);
            }           
            return new()
            {
                Quotes = quotes
            };

        }
    }



}
