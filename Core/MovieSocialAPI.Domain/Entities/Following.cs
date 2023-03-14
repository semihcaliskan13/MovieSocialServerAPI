using MovieSocialAPI.Domain.Entities.Common;
using MovieSocialAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class Following:BaseEntity
    {
        public int UserId { get; set; }
        public int FollowingsId { get; set; }
        public AppUser? User { get; set; }
    }
}
