using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.src.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = _context.Products.Include(p => p.Category).ToList();

            return Task.FromResult(products);
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Include(category => category.Category).FirstOrDefaultAsync(prod => prod.Id == id, cancellationToken);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task Create(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Include(category => category.Category).FirstOrDefaultAsync(prod => prod.Id == id, cancellationToken);
            if (product == null)
            {
                throw new ArgumentException($"L'{id} n'existe pas ou été supprimé");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Product product, CancellationToken cancellationToken)
        {
            _context.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
