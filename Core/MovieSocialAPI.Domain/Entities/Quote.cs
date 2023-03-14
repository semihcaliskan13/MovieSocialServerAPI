using MovieSocialAPI.Domain.Entities.Common;
using MovieSocialAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class Quote:BaseEntity
    {
        public string MovieId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }
        public virtual AppUser? User { get; set; }
        [NotMapped]
        public Movie? Movie { get; set; }
        [NotMapped]
        public int UserDetailId { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}
