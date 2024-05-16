using E_commerceAPI.Models;

namespace E_commerceAPI.Services
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken);
        Task<Payment> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(Payment Payment, CancellationToken cancellationToken);
        Task Update(int id, Payment Payment, CancellationToken cancellationToken);
        Task Save();
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
