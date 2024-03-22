using CoptisTest.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;

namespace CoptisTest.Web.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private User sessionUser = new User { UserId = 0 };
        
        public User GetSessionUser()
        {
            return sessionUser;
        }
        public void SetSessionUserAccesses(List<Access> accesses)
        {
            sessionUser.Accesses = accesses;
        }
        public void ToLogin(User user)
        {

            sessionUser = user;  
        }
        public void ToLogout()
        {
            sessionUser = new User { UserId = 0 };
        }
    }
}
