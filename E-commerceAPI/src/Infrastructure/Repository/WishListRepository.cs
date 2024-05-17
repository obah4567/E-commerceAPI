using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;

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

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var wishList = _context.WishList.Where(w => w.Id == id).FirstOrDefault();
            if (wishList != null)
            {
                _context.WishList.Remove(wishList);
                _context.SaveChanges();

                return Task.CompletedTask;
            }
            else
            {
                return null;
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
                return null;
            }
            return Task.FromResult(wishList);
        }

        public Task Save()
        {
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Update(int id, WishList wishList, CancellationToken cancellationToken)
        {
            var wishL = _context.WishList.Where(w => w.Id == id).FirstOrDefault();
            if (wishL == null)
            {
                return null;
            }
            var updateWishList = new WishListDTO()
            {
                Id = wishList.Id,
                Product_Id = wishList.Product_Id,
                Product = wishList.Product
            };

            _context.Add(updateWishList);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
