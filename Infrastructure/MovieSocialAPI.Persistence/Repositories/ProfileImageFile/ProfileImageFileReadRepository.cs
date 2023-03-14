

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
    public class ProfileImageFileReadRepository : ReadRepository<ProfileImageFile>, IProfileImageFileReadRepository
    {
        public ProfileImageFileReadRepository(MovieSocialAPIDbContext context) : base(context)
        {
        }
    }
}
