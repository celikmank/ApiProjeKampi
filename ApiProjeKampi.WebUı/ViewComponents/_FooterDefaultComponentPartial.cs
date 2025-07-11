using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.ViewComponents
{
    public class _FooterDefaultComponentPartial: ViewComponent 
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
