namespace ApiProjeKampi.WebUı.Models.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    public class _TestimonialDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _TestimonialDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/Testimonials");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiProjeKampi.WebApi.Dtos.TestimonialDtos.ResultTestimonialDto>>(jsonData);
                return View(values); // Pass 'values' to
            }
            return View(); // Return an empty view if the API call fails    

        }
    }
}