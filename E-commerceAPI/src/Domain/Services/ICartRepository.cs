using E_commerceAPI.src.Domain.Models;

namespace E_commerceAPI.src.Domain.Services
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllAsync(CancellationToken cancellationToken);
        Task<Cart> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(Cart cart, CancellationToken cancellationToken);
        Task Update(int id, Cart cart, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Save();
    }
}
