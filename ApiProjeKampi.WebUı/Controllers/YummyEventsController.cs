using Microsoft.AspNetCore.Mvc;
using ApiProjeKampi.WebUı.Dtos.YummyEventDtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUı.Controllers
{
    public class YummyEventsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public YummyEventsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> YummyEventList()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("Https://localhost:7020/api/YummyEvents");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultYummyEventDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateYummyEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateYummyEvent(CreateYummyEventDto createYummyEventDto)
        {
            var client = httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createYummyEventDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7020/api/YummyEvents", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("YummyEventList");
            }
            return View();
        }
        public async Task<IActionResult> DeleteYummyEvent(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7020/api/YummyEvents?id=" + id);
            return RedirectToAction("YummyEventList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateYummyEvent(int id)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/YummyEvents/GetYummyEvent?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultYummyEventDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateYummyEvent(UpdateYummyEventDto updateYummyEventDto)
        {
            var client = httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateYummyEventDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7020/api/YummyEvents", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("YummyEventList");
            }
            return View();
        }
    }
}
