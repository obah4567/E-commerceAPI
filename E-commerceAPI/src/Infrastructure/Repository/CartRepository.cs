using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.src.Infrastructure.Repository
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

        public async Task<Cart> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var getById = await _context.Carts.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
            if (getById == null)
            {
                return null;
            }
            return getById;
        }

        public async Task Create(Cart cart, CancellationToken cancellationToken)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var cart = _context.Carts.Where(c => c.Id == id).FirstOrDefault();
            if (cart == null)
            {
                throw new ArgumentException($"L'{id} n'existe pas ou été supprimé");
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Cart cart, CancellationToken cancellationToken)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
