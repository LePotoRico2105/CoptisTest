using CoptisTest.Models;

namespace CoptisTest.Web.Repositories
{
    public interface ISessionRepository
    {
        User? GetSessionUser();
        void SetSessionUser(User user);
    }
}
