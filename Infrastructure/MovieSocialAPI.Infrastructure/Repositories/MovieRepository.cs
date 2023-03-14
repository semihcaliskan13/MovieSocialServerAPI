using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public long movieId;
        protected string api_key = "";
        

        readonly IConfiguration _configuration;
        public MovieRepository(IConfiguration configuration)
        {
            _configuration= configuration;
            api_key= configuration["API_KEY:api_key"];

        }
        
        public IQueryable<Movie> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<List<MovieCast>> GetCreditsByMovie()
        {
            var client = new RestClient("https://api.themoviedb.org/3/movie/" + movieId + "/credits" + "?api_key=" + api_key + "&language=tr");
            var request = new RestRequest("", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);


                List<Cast> cast = content["cast"].ToObject<List<Cast>>();
                List<Crew> crew = content["crew"].ToObject<List<Crew>>();
                IEnumerable<Cast> casts;
                if (cast.Count>=6)
                {
                    casts = cast.GetRange(0, 6);
                }
                else
                {
                     casts = cast.GetRange(0, cast.Count);
                }
                List<MovieCast> result = new List<MovieCast>();

                foreach (var item in casts)
                {
                    result.Add(new MovieCast { Name = item.original_name, Path = item.profile_path });
                   
                }
                foreach (var crews in crew)
                {
                    if (crews.job== "Original Music Composer" || crews.job=="Director")
                    {
                        result.Add(new MovieCast { Name = crews.name, Job = crews.job, Path = crews.profile_path });
                    }
                }
                Console.Write(result.FirstOrDefault().Name + result.Count.ToString());


                //todo: Buradan devam, oyuncular çekildi fakat composer ve yönetmen filtre ile çekilmeli sonra movieye eklenip gönderilmeli.

                return result;
            }
            return null;
        }
        public async Task<Movie> GetByIdAsync(string id)
        {
            try
            {
                movieId = (long)Convert.ToDouble(id);
            }
            catch (Exception)
            {

                throw;
            }

            List<MovieCast> MovieCredits=await GetCreditsByMovie();
            Movie movie= new Movie();
            foreach (var item in MovieCredits)
            {
               
                if (movie.Composer==null || movie.Director==null)
                {
                    movie.Director = item.Job == "Director" ? item.Name : null;

                    movie.Composer = item.Job == "Original Music Composer" ? item.Name : null;
                }
            }

            var client = new RestClient("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + api_key + "&language=tr");
            var request = new RestRequest("", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                return new Movie
                {
                    MovieId = content["id"].Value<string>(),
                    Title = content["title"].Value<string>(),
                    OriginalTitle = content["original_title"].Value<string>(),
                    Overview = content["overview"].Value<string>(),
                    PosterPath = content["poster_path"].Value<string>(),
                    ReleaseDate = content["release_date"].Value<string>(),
                    Runtime = content["runtime"].Value<string>(),
                    Tagline = content["tagline"].Value<string>(),
                    MovieCast = MovieCredits,
                    Director= movie.Director,
                    Composer= movie.Composer,

                };
            }

            return null;
        }

        public async Task<List<Movie>> GetRecommended()
        {
            Random rnd = new Random();
            List<Movie> movies= new List<Movie>();
            long random = 0;
            long[] randoms = new long[3];
            for (int i = 0; i < randoms.Length; i++)
            {
                random = (long)rnd.Next(1,1000000);
                randoms[i] = random;
            }
            for (int i = 0; i < randoms.Length; i++)
            {
                movies.Add(await GetByIdAsync(randoms[i].ToString()));
            }
            return movies;


        }
    }
}
