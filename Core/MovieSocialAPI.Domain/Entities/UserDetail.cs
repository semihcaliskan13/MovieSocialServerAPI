using MovieSocialAPI.Domain.Entities.Common;
using MovieSocialAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class UserDetail:BaseEntity
    {
       
        public string? WallPaper { get; set; }
        public string? Description { get; set; }
        public string? ProfileImage { get; set; }
        

        public ICollection<AppUser> AppUser { get; set; }



    }
}
