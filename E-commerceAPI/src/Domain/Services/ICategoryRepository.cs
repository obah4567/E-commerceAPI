using E_commerceAPI.src.Domain.Models;

namespace E_commerceAPI.src.Domain.Services
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(Category Category, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Save(CancellationToken cancellationToken);
        Task Update(Category Category, CancellationToken cancellationToken);
    }
}
