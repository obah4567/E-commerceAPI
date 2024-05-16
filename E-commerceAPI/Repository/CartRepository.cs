using E_commerceAPI.DbContexts;
using E_commerceAPI.DTO;
using E_commerceAPI.Models;
using E_commerceAPI.Services;

namespace E_commerceAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _context;

        public CartRepository(Context context)
        {
            _context = context;
        }

        public Task<List<Cart>> GetAllAsync(CancellationToken cancellationToken)
        {
            var carts = _context.Carts.ToList();
            if (carts == null)
            {
                return null;
            }
            return Task.FromResult(carts);
        }

        public Task<Cart> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var cart = _context.Carts.Where(x => x.Id == id).FirstOrDefault();
            if (cart == null)
            {
                return null;
            }
            return Task.FromResult(cart);
        }

        public Task Create(Cart cart, CancellationToken cancellationToken)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var cart = _context.Carts.Where(c => c.Id == id).FirstOrDefault();
            if (cart == null)
            {
                return null;
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Save()
        {
            _context.SaveChanges();

            return Task.CompletedTask;
        }
        public Task Update(int id, Cart cart, CancellationToken cancellationToken)
        {
            var upCart = _context.Carts.Where(c => c.Id == id).FirstOrDefault();
            if (upCart == null)
            {
                return null;
            }
            var updateCart = new CartDTO()
            {
                Id = cart.Id,
                Quantity = cart.Quantity,
                Product_Id = cart.Product_Id
            };
            _context.Add(updateCart);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
