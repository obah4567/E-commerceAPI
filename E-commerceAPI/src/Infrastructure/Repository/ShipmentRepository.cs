using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.src.Infrastructure.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly Context _dbContexts;

        public ShipmentRepository(Context dbContexts)
        {
            _dbContexts = dbContexts;
        }

        public Task<List<Shipment>> GetAllAsync(CancellationToken cancellationToken)
        {
            var shipment = _dbContexts.Shipment.ToList();
            if (shipment == null)
            {
                return null;
            }
            return Task.FromResult(shipment);
        }

        public async Task<Shipment> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var shipment = await _dbContexts.Shipment.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (shipment == null)
            {
                return null;
            }
            return shipment;
        }

        public async Task Create(Shipment Shipment, CancellationToken cancellationToken)
        {
            _dbContexts.Shipment.Add(Shipment);
            _dbContexts.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var shipment = await _dbContexts.Shipment.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (shipment == null)
            {
                throw new ArgumentException($"L'{id} n'existe pas ou été supprimé");
            }
            _dbContexts.Shipment.Remove(shipment);
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _dbContexts.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Shipment shipment, CancellationToken cancellationToken)
        {
            _dbContexts.Shipment.Update(shipment);
            await _dbContexts.SaveChangesAsync(cancellationToken);

        }
    }
}
