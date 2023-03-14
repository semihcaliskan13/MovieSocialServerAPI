using Microsoft.Extensions.DependencyInjection;
using MovieSocialAPI.Application.Abstractions.Storage;
using MovieSocialAPI.Application.Abstractions.Token;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Application.Services;
using MovieSocialAPI.Infrastructure.Enums;
using MovieSocialAPI.Infrastructure.Repositories;
using MovieSocialAPI.Infrastructure.Services;
using MovieSocialAPI.Infrastructure.Services.Storage;
using MovieSocialAPI.Infrastructure.Services.Storage.Local;
using MovieSocialAPI.Infrastructure.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {//DbContext'i IOC Container'a vermek için bu metodu yazdık ve IOC Container'a artık buradan vereceğiz servisleri.
            //Çünkü program.cs karışmaz ve oraya zaten Persistence'ı dahil ettik.         
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        //public static void AddStorage(this IServiceCollection services, StorageType storageType)
        //{
        //    switch (storageType)
        //    {
        //        case StorageType.Local:
        //            services.AddScoped<IStorage, LocalStorage>();

        //            break;
        //        case StorageType.Azure:
        //            //azure

        //            break;
        //        case StorageType.AWS:
        //            break;
        //        default:
        //            services.AddScoped<IStorage, LocalStorage>();

        //            break; 
        //    }
        //}
    }
}
