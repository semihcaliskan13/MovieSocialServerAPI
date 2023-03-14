using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    [Keyless]
    [NotMapped]
    public class Crew
    {
        public string name { get; set; }
        public string adult { get; set; }
        public string gender { get; set; }
        public string id { get; set; }
        public string known_for_department { get; set; }      
        public string original_name { get; set; }
        public string popularity { get; set; }
        public string profile_path { get; set; }
        public string credit_id { get; set; }
        public string department { get; set; }
        public string job { get; set; }


       
    }
}
