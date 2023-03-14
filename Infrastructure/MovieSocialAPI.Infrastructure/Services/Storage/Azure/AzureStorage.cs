using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieSocialAPI.Application.Abstractions.Storage.Azure;

namespace MovieSocialAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage :Storage, IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;//ilgili azure storage account bağlanır
        BlobContainerClient _blobContainerClient;//o accountta hedef container işlerimlerini sağlarız, dosyayı.

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);

        }

        public async Task DeleteAsync(string fileName, string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient= _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient= _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(blobs => blobs.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(blobs => blobs.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);//container yerini aldık.
            await _blobContainerClient.CreateIfNotExistsAsync();//eğer container yoksa oluşturuyoruz.
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);//erişim izni verdik.
            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName=await FileRenameAsync(containerName, file.FileName);
                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
                await blobClient.UploadAsync(file.OpenReadStream());//elimdeki dosyayı azure'a stream olarak gönderelim.
                datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
                
            }
            return datas;   
        }
        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files, string quoteId)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);//container yerini aldık.
            await _blobContainerClient.CreateIfNotExistsAsync();//eğer container yoksa oluşturuyoruz.
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);//erişim izni verdik.
            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(quoteId, file.FileName);
                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
                await blobClient.UploadAsync(file.OpenReadStream());//elimdeki dosyayı azure'a stream olarak gönderelim.
                datas.Add((fileNewName, $"{containerName}/{fileNewName}"));

            }
            return datas;
        }
    }
}
