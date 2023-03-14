using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<MovieSocialAPI.Domain.Entities.File>, IFileWriteRepoistory
    {
        public FileWriteRepository(MovieSocialAPIDbContext context) : base(context)
        {
        }
    }
}
