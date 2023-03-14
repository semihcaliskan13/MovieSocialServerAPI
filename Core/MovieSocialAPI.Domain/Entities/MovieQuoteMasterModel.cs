using MovieSocialAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class MovieQuoteMasterModel
    {
        public Movie? Movie { get; set; }
        public Quote? Quote { get; set; }

        public IQueryable<Movie> MovieList { get; set; }
        public IQueryable<Quote> QuoteList { get; set; }
    }
}
