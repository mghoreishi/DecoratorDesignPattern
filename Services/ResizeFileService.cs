using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace FileUpload.Services
{
    public class ResizeFileService:IFileService
    {
        private readonly IFileService _fileService;

        public ResizeFileService(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var risizeFile =await ResizeFile(file);
            return await _fileService.SaveFileAsync(risizeFile);
        }


        private async Task<IFormFile> ResizeFile(IFormFile file)
        {
            using(var stream=new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var image=SixLabors.ImageSharp.Image.Load(stream);

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new SixLabors.ImageSharp.Size(800, 400),
                    Mode = ResizeMode.Max
                }));

                var resizedStream=new MemoryStream();
                image.Save(resizedStream, new JpegEncoder());
                resizedStream.Position = 0;

                return new FormFile(resizedStream, 0, resizedStream.Length, file.Name, file.FileName);
            }
        }
    }
}

