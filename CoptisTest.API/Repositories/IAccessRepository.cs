using CoptisTest.Models;

namespace CoptisTest.API.Repositories
{
    public interface IAccessRepository
    {
        Task<IEnumerable<Access>> GetAccesses();
        Task<Access?> GetAccess(int accessId);
        Task<IEnumerable<Access>> GetUserAccesses(int userId);
        Task AddAccessToUser(int userId, int accessId);
        Task DeleteAccessToUser(int userId, int accessId);
    }
}
