using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MovieSocialAPI.Application.Services;
using MovieSocialAPI.Infrastructure.Operations;
using System.Text.RegularExpressions;

namespace MovieSocialAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        /*Bu servis bir dosyayı projeye upload etmeye yarıyor. wwwroot içine kaydetme işini görüyor.
         Upload metodu çağırılan talep edilen ilk metot,o diğer iki görevi yapan metotları çağırarak işlemi gerçekleştirecek.
        burada file yazma, path combine etme gibi yapılar durumlar var. Bunlar hepsi bilinmeli.*/
        readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo deneme log
                throw;
            }

        }




        async Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            Regex regex = new Regex("[*'\",+-._&#^@|/<>~]");
            string newFileName = regex.Replace(oldName, string.Empty);
            DateTime datetimenow = DateTime.UtcNow;
            string datetimeutcnow = datetimenow.ToString("yyyyMMddHHmmss");
            string fullName = $"{datetimeutcnow}-{newFileName}{extension}";

            return fullName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            //Tüm dizin ile path'i combine edip birleştiriyor. RootPath wwwroot
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            //eğer dizin yoksa oluştur
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath,file.FileName);
                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

                datas.Add((fileNewName, $"{path}\\{fileNewName}"));

                results.Add(result);


            }
            if (results.TrueForAll(r => r.Equals(true)))
            {
                return datas;
            }
            //todo hata fırlatacağız.Yukarıdaki if geçerli değilse dosyaların sunucuda yüklenirken hata alındığına dair excepion oluştur fırlat.

            return null;


        }


    }
}
