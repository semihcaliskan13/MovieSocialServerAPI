using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Application.Repositories.UserDetail;
using MovieSocialAPI.Domain.Entities;
using MovieSocialAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence.Repositories
{
    public class UserDetailWriteRepository : WriteRepository<UserDetail>, IUserDetailWriteRepository
    {
        public UserDetailWriteRepository(MovieSocialAPIDbContext context) : base(context)
        {
        }
    }
}
