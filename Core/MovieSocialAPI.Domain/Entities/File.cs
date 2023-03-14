using MovieSocialAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Domain.Entities
{
    public class File:BaseEntity
    {//todo bu yapıyı temel olarak açıkla. Bu yapı sayesinde ayırt edici sistem ile neyin neye ait olduğu belli olabiliyor. 
     //Bu proje için örnek bir resim hangi entity'e ait acaba diye aratabilime kolaylığı sağlayacaktır.
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }


        [NotMapped]//override ederek updated eklemeyecek.
        public override DateTime? UpdatedTime { get => base.UpdatedTime; set => base.UpdatedTime = value; }

    }
}
