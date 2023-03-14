using MovieSocialAPI.Domain.Entities.Common;
using MovieSocialAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public AppUser? User { get; set; }
        public Quote? Quote { get; set; }
        public int? UserId { get; set; }
        public int? QuoteId { get; set; }
    }
}
