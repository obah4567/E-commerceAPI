using E_commerceAPI.Models;

namespace E_commerceAPI.Services
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(Product product, CancellationToken cancellationToken);
        Task Update(int id, Product product, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Save();
    }
}
