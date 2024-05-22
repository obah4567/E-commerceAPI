using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.src.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly Context _context;

        public PaymentRepository(Context context)
        {
            _context = context;
        }

        public Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken)
        {
            var payement = _context.Payment.ToList();
            if (payement == null)
            {
                return null;
            }
            return Task.FromResult(payement);
        }


        public async Task<Payment> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var getById = await _context.Payment.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (getById == null)
            {
                return null;
            }
            return getById;
        }


        public async Task Create(Payment payment, CancellationToken cancellationToken)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var payement = await _context.Payment.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (payement == null)
            {
                throw new ArgumentException($"L'{id} n'existe pas ou été supprimé");
            }
            _context.Remove(payement);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Payment payment, CancellationToken cancellationToken)
        {
            _context.Update(payment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
