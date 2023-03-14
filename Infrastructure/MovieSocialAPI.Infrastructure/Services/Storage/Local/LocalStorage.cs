using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MovieSocialAPI.Application.Abstractions.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage :Storage, ILocalStorage
    {
        /*Bu servis bir dosyayı projeye upload etmeye yarıyor. wwwroot içine kaydetme işini görüyor.
         Upload metodu çağırılan talep edilen ilk metot,o diğer iki görevi yapan metotları çağırarak işlemi gerçekleştirecek.
        burada file yazma, path combine etme gibi yapılar durumlar var. Bunlar hepsi bilinmeli.*/
        readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string fileName, string path)
        {
            File.Delete($"{path}\\{fileName}");
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            return dirInfo.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
        {
           return File.Exists($"{path}\\{fileName}");
        }
        private async Task<bool> CopyFileAsync(string path, IFormFile file)
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

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            //Tüm dizin ile path'i combine edip birleştiriyor. RootPath wwwroot
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            //eğer dizin yoksa oluştur
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {

                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
            }

            //todo hata fırlatacağız.Yukarıdaki if geçerli değilse dosyaların sunucuda yüklenirken hata alındığına dair excepion oluştur fırlat.

            return datas;


        }

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files, string quoteId)
        {
            throw new NotImplementedException();
        }
    }
}
