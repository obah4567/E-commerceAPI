using E_commerceAPI.Models;

namespace E_commerceAPI.Services
{
    public interface IShipmentRepository
    {
        Task<List<Shipment>> GetAllAsync(CancellationToken cancellationToken);
        Task<Shipment> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(Shipment Shipment, CancellationToken cancellationToken);
        Task Update(int id, Shipment Shipment, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Save();
    }
}
