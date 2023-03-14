using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using MovieSocialAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence.Repositories
{
    //QuoteReadRepository.cs
    public class QuoteReadRepository : ReadRepository<Quote>, IQuoteReadRepository
    {
        public QuoteReadRepository(MovieSocialAPIDbContext context) : base(context)
        {
        }
    }
}
