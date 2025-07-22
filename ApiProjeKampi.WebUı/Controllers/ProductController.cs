using ApiProjeKampi.WebUı.Dtos.CategoryDtos;
using ApiProjeKampi.WebUı.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebUı.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7020/api/Products");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);

                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/Categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categorieValues = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.CategoryName,
                                                        Value = x.CategoryId.ToString()
                                                    }).ToList();
            ViewBag.v = categorieValues;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto CreateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(CreateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7020/api/Products/CreateProductWithCategory", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7020/api/Products/{id}");
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                // Ürün bilgilerini getir
                var productResponse = await client.GetAsync($"https://localhost:7020/api/Products/GetProduct?id={id}");
                if (!productResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("ProductList");
                }

                var productJsonData = await productResponse.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<GetProductByIdDto>(productJsonData);

                // Kategori listesini getir (dropdown için)
                var categoryResponse = await client.GetAsync("https://localhost:7020/api/Categories");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categoryJsonData = await categoryResponse.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryJsonData);
                    List<SelectListItem> categoryValues = (from x in categories
                                                           select new SelectListItem
                                                           {
                                                               Text = x.CategoryName,
                                                               Value = x.CategoryId.ToString(),
                                                               Selected = x.CategoryId == product.CategoryId
                                                           }).ToList();
                    ViewBag.Categories = categoryValues;
                }

                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Hata: {ex.Message}";
                return RedirectToAction("ProductList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateFeatureDto updateProductDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateProductDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("https://localhost:7020/api/Products", stringContent);
                
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ProductList");
                }
                else
                {
                    ViewBag.Error = "Ürün güncellenirken bir hata oluştu.";
                    
                    // Hata durumunda formu tekrar göstermek için kategori listesini yükle
                    var categoryResponse = await client.GetAsync("https://localhost:7020/api/Categories");
                    if (categoryResponse.IsSuccessStatusCode)
                    {
                        var categoryJsonData = await categoryResponse.Content.ReadAsStringAsync();
                        var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryJsonData);
                        List<SelectListItem> categoryValues = (from x in categories
                                                               select new SelectListItem
                                                               {
                                                                   Text = x.CategoryName,
                                                                   Value = x.CategoryId.ToString(),
                                                                   Selected = x.CategoryId == updateProductDto.CategoryId
                                                               }).ToList();
                        ViewBag.Categories = categoryValues;
                    }
                    
                    // UpdateProductDto'yu GetProductByIdDto'ya çevir
                    var product = new GetProductByIdDto
                    {
                        ProductId = updateProductDto.ProductId,
                        ProductName = updateProductDto.ProductName,
                        ProductDescription = updateProductDto.ProductDescription,
                        Price = updateProductDto.Price,
                        ImageUrl = updateProductDto.ImageUrl,
                        CategoryId = updateProductDto.CategoryId
                    };
                    
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Beklenmeyen hata: {ex.Message}";
                return RedirectToAction("ProductList");
            }
        }
    }
}