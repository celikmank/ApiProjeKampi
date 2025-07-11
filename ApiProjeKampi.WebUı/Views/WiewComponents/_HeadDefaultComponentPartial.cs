using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.Views.WiewComponents
{
    public class _HeadDefaultComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
    
    }

