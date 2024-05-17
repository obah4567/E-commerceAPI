using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;

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

        public Task<Payment> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var payement = _context.Payment.Where(p => p.Id == id).FirstOrDefault();
            if (payement == null)
            {
                return null;
            }
            return Task.FromResult(payement);
        }

        public Task Create(Payment payment, CancellationToken cancellationToken)
        {
            _context.Payment.Add(payment);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var payement = _context.Payment.Where(p => p.Id == id).FirstOrDefault();
            if (payement == null)
            {
                return null;
            }
            _context.Remove(payement);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Save()
        {
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Update(int id, Payment payment, CancellationToken cancellationToken)
        {
            var payement = _context.Payment.Where(p => p.Id == id).FirstOrDefault();
            if (payement == null)
            {
                return null;
            }

            var updatePayment = new PaymentDTO()
            {
                Id = payement.Id,
                Date = payment.Date,
                Method = payment.Method,
                Amount = payment.Amount
            };

            _context.Add(updatePayment);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
