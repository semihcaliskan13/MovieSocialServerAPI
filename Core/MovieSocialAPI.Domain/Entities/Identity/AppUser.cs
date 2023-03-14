using Microsoft.AspNetCore.Identity;

namespace MovieSocialAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        

        public ICollection<Quote>? Quotes { get; set; }

        public UserDetail UserDetail { get; set; }
        public int UserDetailId { get; set; }


        public ICollection<MovieList>? MovieLists { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<Follower>? Followers { get; set; }
        public ICollection<Following>? Followings { get; set; }


    }
}
