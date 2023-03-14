using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence.Repositories.File
{
    public class FileReadRepository : ReadRepository<MovieSocialAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(MovieSocialAPIDbContext context) : base(context)
        {
        }
    }
}
