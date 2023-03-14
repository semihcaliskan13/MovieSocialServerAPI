using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieSocialAPI.Domain.Entities;
using MovieSocialAPI.Domain.Entities.Common;
using MovieSocialAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence.Contexts
{
    public class MovieSocialAPIDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public MovieSocialAPIDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Follower> Follower { get; set; }
        public DbSet<Following> Following { get; set; }



        public DbSet<MovieList> MovieLists { get; set; }
        public DbSet<MovieListMovie> MovieListMovies { get; set; }
        public DbSet<UserDetail> UserDetails{ get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }//Bu 3 yapı table per hierarchy şekilnde inşa edilecek.
        public DbSet<ProfileImageFile> ProfileImageFiles { get; set; }
        public DbSet<ProfileBackgroundImageFile> ProfileBackgroundImageFiles { get; set; }

        
        


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker entity üzerinden yapılan değişiklerin veya yeni eklenen verinin yakalanmasını sağlayan prop.
            //update operasyonlarında track edilen verileri yakalayıp elde etmemizi sağlar.
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                     EntityState.Added=>data.Entity.CreatedTime=DateTime.Now,
                     EntityState.Modified=>data.Entity.UpdatedTime=DateTime.Now,
                     _=>DateTime.Now,
                };
            }


            return await base.SaveChangesAsync(cancellationToken);
        }
        
    }
}
