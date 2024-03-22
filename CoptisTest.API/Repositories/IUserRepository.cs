using CoptisTest.Models;

namespace CoptisTest.API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(int userId);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
