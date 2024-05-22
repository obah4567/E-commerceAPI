using E_commerceAPI.src.Domain.Models;

namespace E_commerceAPI.src.Domain.Services
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken);
        Task<Payment> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(Payment Payment, CancellationToken cancellationToken);
        Task Update(Payment Payment, CancellationToken cancellationToken);
        Task Save(CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
