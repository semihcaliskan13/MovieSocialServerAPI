using Microsoft.Extensions.Configuration;//cfg için gerekli.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Persistence
{
    static class Configuration
    {
        //Bu metotda Connection string property yapısı oluşturduk. Bu yapıyı ServiceRegistiration içine vereceğiz.
        //.Get isteğine string gidecek ve ServiceRegistiration ile program.cs içine servis olarak eklenecektir.
        static public string ConnectionString
        {
            get
            {
            
                ConfigurationManager cfg = new();
                cfg.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MovieSocialAPI.API"));
                cfg.AddJsonFile("appsettings.json");//microsoft.extensions.configuration.json adındaki paket üst 2 satır için gerekli.

                return cfg.GetConnectionString("MicrosoftSQL");
            }
        }
    }
}
