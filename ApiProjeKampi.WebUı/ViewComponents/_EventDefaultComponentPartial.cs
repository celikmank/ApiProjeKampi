using ApiProjeKampi.WebApi.Dtos.EventDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.ViewComponents
{
    public class _EventDefaultComponentPartial :ViewComponent
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public _EventDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/YummyEvents");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEventDtos>>(jsonData);
                return View(values); // Pass 'values' to the view
            }
            return View(); // Return an empty view if the API call fails
        }
    }
}
