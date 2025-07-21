using ApiProjeKampi.WebUı.Dtos.CategoryDtos;
using ApiProjeKampi.WebUı.Dtos.FeatureDtos;
using ApiProjeKampi.WebUı.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUı.Controllers
{
    public class FeatureController :Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> FeatureList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7020/api/Features");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);

                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateFeature()
        { return View(); }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateFeatureDto createFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7020/api/Features", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }
            return View();
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7020/api/Features?id=" + id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/GetFeature?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetCategoryByIdDto>(jsonData);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7020/api/Features/", stringContent);
            return RedirectToAction("CategoryList");
        }
    }
}
