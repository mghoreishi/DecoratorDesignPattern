using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileUpload.Services
{
    public class RenameFileService : IFileService
    {
        private readonly IFileService _fileService;

        public RenameFileService(IFileService fileService)
        {
            _fileService = fileService;
        }
        public Task<string> SaveFileAsync(IFormFile file)
        {
            var renameFile = RenameFile(file);
            return _fileService.SaveFileAsync(renameFile);
         }


        private IFormFile RenameFile(IFormFile file)
        {
            string extesion = Path.GetExtension(file.FileName);
            string prefix = "file_";
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm");
            string newFileName = prefix + timestamp + extesion;

            byte[] fileBytes;
            using (var memoryStream=new MemoryStream())
            {
                file.CopyTo(memoryStream);  
                fileBytes = memoryStream.ToArray();
            }
         

            return new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", newFileName);

        }
    }
}
