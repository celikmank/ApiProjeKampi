using ApiProjeKampi.WebUı.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.ViewComponents
{
    public class _ContactDefaultComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new ContactViewModel();
            return View(model);
        }
    }
}