using E_commerceAPI.src.Domain.Models;

namespace E_commerceAPI.src.Domain.Services
{
    public interface IWishListRepository
    {
        Task<List<WishList>> GetAllAsync(CancellationToken cancellationToken);
        Task<WishList> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(WishList WishList, CancellationToken cancellationToken);
        Task Update(WishList WishList, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Save(CancellationToken cancellationToken);
    }
}
