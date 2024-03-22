using CoptisTest.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoptisTest.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CoptisTestContext _context;

        public UserRepository(CoptisTestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetUser(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task AddUser(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUser(User user)
        {
            var result = await _context.Users
                .FirstAsync(e => e.UserId == user.UserId);
            result.Username = user.Username;
            result.Password = user.Password;
            result.Accesses = user.Accesses;

            await _context.SaveChangesAsync();           
        }
        public async Task DeleteUser(int userId)
        {
            var result = await _context.Users
                .FirstAsync(e => e.UserId == userId);
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
