using E_commerceAPI.DbContexts;
using E_commerceAPI.DTO;
using E_commerceAPI.Models;
using E_commerceAPI.Services;

namespace E_commerceAPI.Repository
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

        public Task<Shipment> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var shipment = _dbContexts.Shipment.Where(s => s.Id == id).FirstOrDefault();
            if (shipment == null)
            {
                return null;
            }
            return Task.FromResult(shipment);
        }

        public Task Create(Shipment Shipment, CancellationToken cancellationToken)
        {
            _dbContexts.Shipment.Add(Shipment);
            _dbContexts.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var shipment = _dbContexts.Shipment.Where(s => s.Id == id).FirstOrDefault();
            if (shipment == null)
            {
                return null;
            }
            _dbContexts.Shipment.Remove(shipment);
            _dbContexts.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Save()
        {
            _dbContexts.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Update(int id, Shipment shipment, CancellationToken cancellationToken)
        {
            var upSipment = _dbContexts.Shipment.Where(s => s.Id == id).FirstOrDefault();
            if (upSipment == null)
            {
                return null;
            }

            var updateShipment = new ShipmentDTO()
            {
                Id = shipment.Id,
                Date = shipment.Date,
                Address = shipment.Address,
                City = shipment.City,
                Region = shipment.Region,
                Code_Postal = shipment.Code_Postal
            };

            _dbContexts.Add(updateShipment);
            _dbContexts.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
