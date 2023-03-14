using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    
    public class Movie
    {
        public string Title{ get; set; }
        public string OriginalTitle { get; set; }
        public string MovieId { get; set; }
        public string Overview { get; set; }       
        public string PosterPath { get; set; }
        public string ReleaseDate { get; set; }
        public string Runtime { get; set; }
        public string Tagline { get; set; }
        public string Director { get; set; }
        public string Composer { get; set; }
        public string Screenplay { get; set; }
        
        public List<MovieCast> MovieCast { get; set; }
       



    }
}
