using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Queries.GetAllQuotes
{
    public class GetAllQutesQueryHandler : IRequestHandler<GetAllQutoesQueryRequest, GetAllQuotesQueryResponse>
    {
        readonly IQuoteReadRepository _quoteReadRepository;
        readonly IMovieRepository _movieRepository;
        public GetAllQutesQueryHandler(IQuoteReadRepository quoteReadRepository, IMovieRepository movieRepository)
        {
            _quoteReadRepository = quoteReadRepository;
            _movieRepository = movieRepository;
        }

        public async Task<GetAllQuotesQueryResponse> Handle(GetAllQutoesQueryRequest request, CancellationToken cancellationToken)
        {


            IQueryable<Quote> datas = _quoteReadRepository.Table.Include(favorite => favorite.Favorites);

            foreach (var quote in datas)
            {
                quote.Movie = await _movieRepository.GetByIdAsync(quote.MovieId);
            }

            IQueryable<Quote> quotes=datas.Select(data => new Quote
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
                Quotes = quotes
            };
        }
    }
}
