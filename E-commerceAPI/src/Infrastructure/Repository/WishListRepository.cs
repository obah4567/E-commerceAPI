using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.src.Infrastructure.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly Context _context;

        public WishListRepository(Context context)
        {
            _context = context;
        }

        public Task Create(WishList WishList, CancellationToken cancellationToken)
        {
            _context.WishList.Add(WishList);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var wishList = await _context.WishList.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
            if (wishList != null)
            {
                _context.WishList.Remove(wishList);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Don't found ");
            }
        }

        public Task<List<WishList>> GetAllAsync(CancellationToken cancellationToken)
        {
            var wishList = _context.WishList.ToList();

            return Task.FromResult(wishList);
        }

        public Task<WishList> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var wishList = _context.WishList.Where(w => w.Id == id).FirstOrDefault();
            if (wishList == null)
            {
                throw new ArgumentException($"{wishList} not exist");
            }
            return Task.FromResult(wishList);
        }

        public async Task Save(CancellationToken cancellation)
        {
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task Update(WishList wishList, CancellationToken cancellationToken)
        {
            _context.WishList.Update(wishList);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
