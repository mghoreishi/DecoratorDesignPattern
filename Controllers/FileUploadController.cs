using System.Threading.Tasks;
using FileUpload.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUpload.Controllers
{
	public class FileUploadController : Controller
    {


		private readonly IFileService _fileService;

        public FileUploadController(IFileService fileService)
        {
			//_fileService = fileService;

			//_fileService = new RenameFileService(fileService);

			_fileService = new ResizeFileService(fileService);
		}

        [HttpPost("FileUpload")]
		public async Task<IActionResult> Index(IFormFile file)
		{			
			var filePath= await _fileService.SaveFileAsync(file);

			return View("Index", filePath);
			
		}

	}
}