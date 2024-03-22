using CoptisTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CoptisTest.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CoptisTestContext _context;

        public OrderRepository(CoptisTestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order?> GetOrder(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
        public async Task AddOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOrder(int orderId)
        {
            var result = await _context.Orders
                .FirstAsync(e => e.OrderId == orderId);
            _context.Orders.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
