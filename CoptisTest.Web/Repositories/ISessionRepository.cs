using CoptisTest.Models;

namespace CoptisTest.Web.Repositories
{
    public interface ISessionRepository
    {
        User GetSessionUser();
        void SetSessionUserAccesses(List<Access> accesses);
        void ToLogin(User user);
        void ToLogout();
    }
}
