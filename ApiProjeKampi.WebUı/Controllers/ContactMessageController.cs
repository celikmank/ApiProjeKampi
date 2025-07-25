using ApiProjeKampi.WebUı.Dtos.ContactDtos;
using ApiProjeKampi.WebUı.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ApiProjeKampi.WebUı.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:7020/api/ContactMessages", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Success = "Mesajınız başarıyla gönderildi.";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Error = "Mesaj gönderilirken bir hata oluştu.";
            }

            return View();
        }
    }
}
