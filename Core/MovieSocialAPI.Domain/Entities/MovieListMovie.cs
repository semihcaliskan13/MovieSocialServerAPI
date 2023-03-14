using MovieSocialAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class MovieListMovie:BaseEntity
    {
        public string MovieTMDBId { get; set; }
        public ICollection<MovieList>? MovieLists { get; set; }
    }
}
