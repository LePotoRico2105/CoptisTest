using CoptisTest.API.Controllers;
using CoptisTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CoptisTest.API.Repositories
{
    public class AccessRepository : IAccessRepository
    {
        private readonly CoptisTestContext _context;

        public AccessRepository(CoptisTestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Access>> GetAccesses()
        {
            return await _context.Accesses.ToListAsync();
        }
        public async Task<Access?> GetAccess(int accessId)
        {
            return await _context.Accesses.FirstOrDefaultAsync(u => u.AccessId == accessId);
        }
        public async Task<IEnumerable<Access>> GetUserAccesses(int userId)
        {
            return await _context.Users.Where(x => x.UserId == userId).SelectMany(x => x.Accesses).ToListAsync();
        }
        public async Task AddAccessToUser(int userId, int accessId)
        {
            var access = await _context.Accesses.FirstAsync(u => u.AccessId == accessId);
            var user = await _context.Users.Include(u => u.Accesses).FirstAsync(e => e.UserId == userId);
            user.Accesses.Add(access);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAccessToUser(int userId, int accessId)
        {
            var access = await _context.Accesses.FirstAsync(u => u.AccessId == accessId);
            var user = await _context.Users.Include(u => u.Accesses).FirstAsync(e => e.UserId == userId);
            user.Accesses.Remove(access);
            await _context.SaveChangesAsync();
        }
    }
}
