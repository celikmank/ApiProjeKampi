using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
