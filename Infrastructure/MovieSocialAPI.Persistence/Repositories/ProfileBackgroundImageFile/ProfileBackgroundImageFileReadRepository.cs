﻿using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using MovieSocialAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence.Repositories
{
    public class ProfileBackgroundImageFileReadRepository : ReadRepository<ProfileBackgroundImageFile>, IProfileBackgroundImageFileReadRepository
    {
        public ProfileBackgroundImageFileReadRepository(MovieSocialAPIDbContext context) : base(context)
        {
        }
    }
}
