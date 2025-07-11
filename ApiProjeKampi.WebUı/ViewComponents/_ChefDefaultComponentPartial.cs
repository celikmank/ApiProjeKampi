using ApiProjeKampi.WebApi.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.ViewComponents
{
    public class _ChefDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ChefDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/Chefs");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultChefDto>>(JsonData);
                return View(values);
            }
            return View();
        }

    }
}
