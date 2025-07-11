using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUı.ViewComponents._DefaultMenuViewComponent
{
    public class _DefaultMenuCategoryComponentPartial : ViewComponent
    {      
            private readonly IHttpClientFactory _httpClientFactory;
            public _DefaultMenuCategoryComponentPartial(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }
            public async Task<IViewComponentResult> InvokeAsync()
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7020/api/Categories/");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ApiProjeKampi.WebApi.Dtos.CategoryDtos.ResultCategoryDto>>(jsonData);
                    return View(values); // Pass 'values' to    

                }
                return View();
            }
        }
}
