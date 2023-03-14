using Microsoft.AspNetCore.Http;
using MovieSocialAPI.Application.Abstractions.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storageService;

        public StorageService(IStorage storageService)
        { 
            _storageService = storageService;
        }

        public string StorageName { get => _storageService.GetType().Name; }

        public async Task DeleteAsync(string fileName, string pathOrContainerName)
        =>await _storageService.DeleteAsync(fileName, pathOrContainerName);

        public  List<string> GetFiles(string pathOrContainerName)
        =>  _storageService.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
        =>_storageService.HasFile(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        =>_storageService.UploadAsync(pathOrContainerName, files);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files,string quoteId)
        => _storageService.UploadAsync(pathOrContainerName, files,quoteId);
    }
}
