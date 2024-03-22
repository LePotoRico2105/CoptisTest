using CoptisTest.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoptisTest.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<User[]>("User/GetUsers");
            if (result == null) return new List<User>();
            else return result;
        }
        public async Task<User?> GetUser(int userId)
        {
            return await _httpClient.GetFromJsonAsync<User>($"User/GetUser/{userId}");
        }
        public async Task<HttpResponseMessage> AddUser(User user)
        {
            var content = JsonConvert.SerializeObject(user);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await _httpClient.PostAsync($"User/AddUser", byteContent);
        }
    }
}
