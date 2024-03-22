using CoptisTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CoptisTest.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CoptisTestContext _context;

        public ProductRepository(CoptisTestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product?> GetProduct(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        }
        public async Task AddProduct(Product product)
        {
            var result = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            var result = await _context.Products
                .FirstAsync(e => e.ProductId == product.ProductId);
            result.Name = product.Name;
            result.Quantity = product.Quantity;
            result.Price = product.Price;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteProduct(int productId)
        {
            var result = await _context.Products
                .FirstAsync(e => e.ProductId == productId);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
