using E_commerceAPI.Models;

namespace E_commerceAPI.Services
{
    public interface IWishListRepository
    {
        Task<List<WishList>> GetAllAsync(CancellationToken cancellationToken);
        Task<WishList> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(WishList WishList, CancellationToken cancellationToken);
        Task Update(int id, WishList WishList, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Save();
    }
}
