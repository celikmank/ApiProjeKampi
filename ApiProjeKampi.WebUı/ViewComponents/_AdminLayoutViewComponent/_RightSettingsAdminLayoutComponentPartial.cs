using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ApiProjeKampi.WebUı.ViewComponents._AdminLayoutViewComponent
{
    public class _RightSettingsAdminLayoutComponentPartial :ViewComponent 
    {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
