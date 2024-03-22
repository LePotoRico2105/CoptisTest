using CoptisTest.Models;

namespace CoptisTest.Web.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order?> GetOrder(int orderId);
        Task<HttpResponseMessage> AddOrder(Order order);
        Task<HttpResponseMessage> DeleteOrder(int orderId);
    }
}
