using CoptisTest.Models;

namespace CoptisTest.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(int userId);
        Task<HttpResponseMessage> AddUser(User user);
    }
}
