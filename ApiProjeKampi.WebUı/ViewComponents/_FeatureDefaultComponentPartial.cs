using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.ViewComponents
{
    public class _FeatureDefaultComponentPartial: ViewComponent
    {
        public  IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
