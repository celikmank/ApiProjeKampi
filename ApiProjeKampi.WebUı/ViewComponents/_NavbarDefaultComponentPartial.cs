using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.ViewComponents
{
    public class _NavbarDefaultComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(); 
        }
    }
}
