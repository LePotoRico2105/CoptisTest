using CoptisTest.Models;

namespace CoptisTest.Web.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProduct(int productId);
        Task<HttpResponseMessage> AddProduct(Product product);
        Task<HttpResponseMessage> UpdateProduct(Product product);
        Task<HttpResponseMessage> DeleteProduct(int productId);
    }
}
