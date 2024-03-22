using CoptisTest.Models;

namespace CoptisTest.Web.Repositories
{
    public interface IAccessRepository
    {
        Task<IEnumerable<Access>> GetAccesses();
        Task<Access?> GetAccess(int accessId);
        Task<IEnumerable<Access>> GetUserAccesses(int userId);
        Task<HttpResponseMessage> AddAccessToUser(int userId, int accessId);
        Task<HttpResponseMessage> DeleteAccessToUser(int userId, int accessId);
    }
}
