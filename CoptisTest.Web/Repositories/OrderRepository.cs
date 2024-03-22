using CoptisTest.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CoptisTest.Web.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HttpClient _httpClient;

        public OrderRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var result = await _httpClient.GetFromJsonAsync<Order[]>("Order/GetOrders");
            if (result == null) return new List<Order>();
            else return result;
        }
        public async Task<Order?> GetOrder(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<Order>($"Order/GetOrder/{orderId}");
        }
        public async Task<HttpResponseMessage> AddOrder(Order order)
        {
            var content = JsonConvert.SerializeObject(order);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await _httpClient.PostAsync($"Order/AddOrder", byteContent);
        }
        public async Task<HttpResponseMessage> DeleteOrder(int orderId)
        {
            return await _httpClient.DeleteAsync($"Order/DeleteOrder/{orderId}");
        }
    }
}
