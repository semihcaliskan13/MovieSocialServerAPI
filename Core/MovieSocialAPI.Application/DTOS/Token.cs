using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.DTOS
{
    public class Token
    {
        public string AccesssToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
