using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.Controllers
{
    public class AdminController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
