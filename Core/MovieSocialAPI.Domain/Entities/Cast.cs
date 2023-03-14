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
    public class Cast
    {
        public string original_name { get; set; }
        public string profile_path { get; set; }

        public string adult;
        public string gender;
        public string id;
        public string known_for_department;

        
        public string popularity;

        public string cast_id;
        public string character;
        public string credit_id;
        public string order;
    }
    
    

}
