using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileUpload.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
