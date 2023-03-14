using MovieSocialAPI.Domain.Entities.Common;
using MovieSocialAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class MovieList:BaseEntity
    {

        public string MovieListName { get; set; }
        public ICollection<MovieListMovie>? MovieListMovies{ get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
