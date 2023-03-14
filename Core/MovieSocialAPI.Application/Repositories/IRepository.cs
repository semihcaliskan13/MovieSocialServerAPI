using Microsoft.EntityFrameworkCore;
using MovieSocialAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Repositories
{
    //IRepository.cs
    public interface IRepository<T> where T : BaseEntity
    {
      DbSet<T> Table { get; }
    }
}
