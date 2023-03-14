using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Queries.GetAllQuotes
{
    public class GetAllQuotesQueryResponse
    {
        public IQueryable<Quote>? Quotes { get; set; }   
    }
}
