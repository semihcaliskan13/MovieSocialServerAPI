using MovieSocialAPI.Domain.Entities;
using MovieSocialAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Repositories
{
    public interface IFollowerReadRepository: IReadRepository<Follower> 
    {
    }
}
