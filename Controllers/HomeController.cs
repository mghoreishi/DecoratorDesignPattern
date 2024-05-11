using Microsoft.AspNetCore.Mvc;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
     
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

      
    }
}
