using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieSocialAPI.Application.Features.Queries.GetAllQuotes;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Queries.GetQuotesBuUserId
{
    public class GetQuotesByUserIdQueryRequest : IRequest<GetQuotesByUserIdQueryResponse>
    {
        public string UserId { get; set; }
    }
    public class GetQuotesByUserIdQueryResponse
    {
        public IQueryable<Quote>? Quotes { get; set; }
    }
    public class GetQuotesByUserIdQueryHandler : IRequestHandler<GetQuotesByUserIdQueryRequest, GetQuotesByUserIdQueryResponse>
    {
        private readonly IQuoteReadRepository _quoteReadRepository;
        private readonly IMovieRepository _movieRepository;

        public GetQuotesByUserIdQueryHandler(IQuoteReadRepository quoteReadRepository, IMovieRepository movieRepository)
        {
            _quoteReadRepository = quoteReadRepository;
            _movieRepository = movieRepository;
        }

        public async Task<GetQuotesByUserIdQueryResponse> Handle(GetQuotesByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = _quoteReadRepository.Table.Include(favorite => favorite.Favorites)
                .Where(x => x.UserId == int.Parse(request.UserId)).Select(data => new
                {
                    data.Id,
                    data.CreatedTime,
                    data.Favorites,
                    data.ImagePath,
                    data.UserId,
                    data.Description,
                    data.UpdatedTime,
                    data.Movie,
                    data.MovieId,
                    data.User.UserDetailId

                }).ToList();
            IQueryable<Quote> quotes = _quoteReadRepository.GetWhere(data => data.UserId == int.Parse(request.UserId)).Include(x => x.User).Include(fav=>fav.Favorites);

            foreach (var quote in quotes)
            {
                quote.Movie = await _movieRepository.GetByIdAsync(quote.MovieId);
            }
            IQueryable<Quote> quotes2 = quotes.Select(data => new Quote
            {
                Id = data.Id,
                CreatedTime = data.CreatedTime,
                Favorites = data.Favorites,
                ImagePath = data.ImagePath,
                UserId = data.UserId,
                Description = data.Description,
                UpdatedTime = data.UpdatedTime,
                Movie = data.Movie,
                MovieId = data.MovieId,
                UserDetailId = data.User.UserDetailId

            });

           
            return new()
            {
                Quotes = quotes2
            };
        }
    }
}
