using CoptisTest.Models;

namespace CoptisTest.API.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order?> GetOrder(int orderId);
        Task AddOrder(Order order);
        Task DeleteOrder(int orderId);
    }
}
