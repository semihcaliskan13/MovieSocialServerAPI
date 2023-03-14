using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOS.Token CreateAccess(int minute, string userId);
    }
}
