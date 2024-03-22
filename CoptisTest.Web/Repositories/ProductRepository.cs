using CoptisTest.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CoptisTest.Web.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;

        public ProductRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<Product[]>("Product/GetProducts");
            if (result == null) return new List<Product>();
            else return result;
        }
        public async Task<Product?> GetProduct(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"Product/GetProduct/{productId}");
        }
        public async Task<HttpResponseMessage> AddProduct(Product product)
        {
            var content = JsonConvert.SerializeObject(product);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await _httpClient.PostAsync($"Product/AddProduct", byteContent);
        }
        public async Task<HttpResponseMessage> UpdateProduct(Product product)
        {
            var content = JsonConvert.SerializeObject(product);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await _httpClient.PutAsync($"Product/UpdateProduct", byteContent);
        }
        public async Task<HttpResponseMessage> DeleteProduct(int productId)
        {
            return await _httpClient.DeleteAsync($"Product/DeleteProduct/{productId}");
        }
    }
}
