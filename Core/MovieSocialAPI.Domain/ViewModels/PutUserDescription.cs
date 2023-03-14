using MovieSocialAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.ViewModels
{
    public class PutUserDescription
    {
        public string? Description { get; set; }
        public string? WallPaper { get; set; }
        public DateTime? CreatedTime { get; set; }
        virtual public DateTime? UpdatedTime { get; set; }
        public string? ProfileImage { get; set; }
    }
}
