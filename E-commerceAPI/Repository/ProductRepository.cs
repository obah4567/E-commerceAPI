using E_commerceAPI.DbContexts;
using E_commerceAPI.DTO;
using E_commerceAPI.Models;
using E_commerceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.Repository
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

        public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = _context.Products.Include(category => category.Category).Where(prod => prod.Id == id).FirstOrDefault();
            if (product == null)
            {
                return null;
            }
            return Task.FromResult(product);
        }

        public Task Create(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var product = _context.Products.Include(category => category.Category).Where(prod => prod.Id == id).FirstOrDefault();
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Save()
        {
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Update(int id, Product product, CancellationToken cancellationToken)
        {
            var updateProduct = _context.Products.Include(category => category.Category).Where(prod => prod.Id == id).FirstOrDefault();
            if (updateProduct == null)
            {
                return null;
            }

            var updateProductDto = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,

                Category_Id = product.Category_Id
            };

            _context.Add(updateProductDto);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
