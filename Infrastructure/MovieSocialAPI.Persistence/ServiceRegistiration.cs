using Microsoft.EntityFrameworkCore;//Bu dahil edilmeli.
using Microsoft.Extensions.Configuration;//Bunun paketi yüklenmeli ve dahil edilmeli.
using Microsoft.Extensions.DependencyInjection;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Application.Repositories.MovieLists;
using MovieSocialAPI.Application.Repositories.UserDetail;
using MovieSocialAPI.Domain.Entities.Identity;
using MovieSocialAPI.Persistence.Contexts;
using MovieSocialAPI.Persistence.Repositories;
using MovieSocialAPI.Persistence.Repositories.File;
using MovieSocialAPI.Persistence.Repositories.MovieLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence
{
    public static class ServiceRegistiration
    {
    public static void AddPersistenceServices(this IServiceCollection services)
        {//DbContext'i IOC Container'a vermek için bu metodu yazdık ve IOC Container'a artık buradan vereceğiz servisleri.
            //Çünkü program.cs karışmaz ve oraya zaten Persistence'ı dahil ettik.
            services.AddDbContext<MovieSocialAPIDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            
            services.AddScoped<IQuoteReadRepository,QuoteReadRepository>();
            services.AddScoped<IQuoteWriteRepository, QuoteWriteRepository>();


            services.AddScoped<IFollowerReadRepository, FollowerReadRepository>();
            services.AddScoped<IFollowerWriteRepository, FollowerWriteRepository>();


            services.AddScoped<IFollowingReadRepository, FollowingReadRepository>();
            services.AddScoped<IFollowingWriteRepository, FollowingWriteRepository>();

            




            services.AddScoped<IFileReadRepository,FileReadRepository>();
            services.AddScoped<IFileWriteRepoistory,FileWriteRepository>();
            services.AddScoped<IProfileImageFileReadRepository,ProfileImageFileReadRepository>();
            services.AddScoped<IProfileImageFileWriteRepository,ProfileImageFileWriteRepository>();
            services.AddScoped<IProfileBackgroundImageFileReadRepository,ProfileBackgroundImageFileReadRepository>();
            services.AddScoped<IProfileBackgroundImageFileWriteRepository,ProfileBackgroundImageFileWriteRepository>();
            services.AddScoped<IUserDetailReadRepository,UserDetailReadRepository>();
            services.AddScoped<IUserDetailWriteRepository,UserDetailWriteRepository>();

            services.AddScoped<IMovieListsReadRepository, MovieListsReadRepository>();
            services.AddScoped<IMovieListsWriteRepository, MovieListWriteRepository>();

            services.AddScoped<IFavoriteWriteRepository,FavoriteWriteRepository>();
            services.AddScoped<IFavoriteReadRepository,FavoriteReadRepository>();


            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail= true;
                
                
            }).AddEntityFrameworkStores<MovieSocialAPIDbContext>();

        }
    }
}
