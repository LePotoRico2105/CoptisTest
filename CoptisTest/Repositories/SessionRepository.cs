using CoptisTest.Models;

namespace CoptisTest.Web.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private User? sessionUser;
        public User? GetSessionUser()
        {
            return sessionUser;
        }
        public void SetSessionUser(User user)
        {
            sessionUser = user;
        }
    }
}
