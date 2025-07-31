using ApiProjeKampi.WebUı.Dtos.AboutDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebUı.Controllers // 'Uı' namespace ile uyumlu
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Listeleme
        public async Task<IActionResult> AboutList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7020/api/Abouts");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }
            // Hata durumunda boş liste dön
            return View(new List<ResultAboutDto>());
        }

        // Ekleme (GET)
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        // Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createAboutDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7020/api/Abouts", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }

            // Başarısız olursa formu tekrar göster
            return View(createAboutDto);
        }

        // Silme
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7020/api/Abouts?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }
            // Hata sayfasına yönlendirilebilir (isteğe bağlı)
            return View("Error");
        }

        // Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7020/api/Abouts/GetAbout?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetAboutByIdDto>(jsonData);
                return View(value);
            }

            return View("Error");
        }

        // Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAboutDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7020/api/Abouts", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }

            // Başarısız olursa kullanıcıya aynı formu geri göster
            return View(updateAboutDto);
        }
    }
}
