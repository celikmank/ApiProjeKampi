using ApiProjeKampi.WebApi.Dtos.ChefDtos;
using ApiProjeKampi.WebApi.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUı.ViewComponents._AdminLayoutNavbarViewComponent
{
    public class _NavbarMessageListAdminLayoutComponentPartial :ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _NavbarMessageListAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/Messages/MessageListByIsReadFalse");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalse>>(JsonData);
                return View(values);
            }
            return View();
        }

    }
}

