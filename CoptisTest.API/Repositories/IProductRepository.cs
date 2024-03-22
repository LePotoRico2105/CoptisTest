using CoptisTest.Models;

namespace CoptisTest.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProduct(int productId);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int productId);
    }
}
