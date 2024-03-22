using CoptisTest.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Nodes;

namespace CoptisTest.Web.Repositories
{
    public class AccessRepository : IAccessRepository
    {
        private readonly HttpClient _httpClient;

        public AccessRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Access>> GetAccesses()
        {
            var result = await _httpClient.GetFromJsonAsync<Access[]>("Access/GetAccesses");
            if (result == null) return new List<Access>();
            else return result;
        }
        public async Task<Access?> GetAccess(int accessId)
        {
            return await _httpClient.GetFromJsonAsync<Access>($"Access/GetAccess/{accessId}");
        }
        public async Task<IEnumerable<Access>> GetUserAccesses(int userId)
        {
            var result = await _httpClient.GetFromJsonAsync<Access[]>($"Access/GetUserAccesses/{userId}");
            if (result == null) return new List<Access>();
            else return result;
        }
        public async Task<HttpResponseMessage> AddAccessToUser(int userId, int accessId)
        {
            var myObject = (dynamic)new JsonObject();
            myObject.Data = userId;
            myObject.Data2 = accessId;
            var content = new StringContent(myObject.ToString(), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"Access/AddUser", content);
        }
        public async Task<HttpResponseMessage> DeleteAccessToUser(int userId, int accessId)
        {
            return await _httpClient.DeleteAsync($"Access/DeleteAccessToUser?userId={userId}&accessId={accessId}");
        }
    }
}
