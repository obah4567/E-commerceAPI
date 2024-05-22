using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.src.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private IShipmentRepository _ShipmentRepository;
        private ILogger<ShipmentController> _logger;

        public ShipmentController(IShipmentRepository ShipmentRepository, ILogger<ShipmentController> logger)
        {
            _ShipmentRepository = ShipmentRepository ?? throw new ArgumentNullException(nameof(_ShipmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Shipment>> GetAll(CancellationToken cancellationToken)
        {
            var Shipments = await _ShipmentRepository.GetAllAsync(cancellationToken);
            if (Shipments == null)
            {
                return NotFound();
            }
            return Ok(Shipments);
        }

        [HttpGet("{id}", Name = "GetShipmentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Shipment>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var Shipment = await _ShipmentRepository.GetByIdAsync(id, cancellationToken);
                if (Shipment == null)
                {
                    return NotFound();
                }
                return Ok(Shipment);
            }
            catch (Exception)
            {
                _logger.LogError($"L'id {id} n\'existe pas");
                return StatusCode(StatusCodes.Status404NotFound, "L'Id n'a été trouvé");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Shipment>> Create([FromBody] ShipmentDTO Shipment, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var existingShipment = await _ShipmentRepository.GetByIdAsync(Shipment.Id, cancellationToken);
                if (existingShipment != null)
                {
                    return Conflict( new { message = $"Shipment {Shipment.Id} already exists"});
                }

                var createShipment = new Shipment()
                {
                    Id = Shipment.Id,
                    Date = DateTime.UtcNow, // a tester si la date de creation de maintenant ou aux choix 
                    Address = Shipment.Address,
                    City = Shipment.City,
                    Region = Shipment.Region,
                    Country = Shipment.Country,
                    Code_Postal = Shipment.Code_Postal
                };

                await _ShipmentRepository.Create(createShipment, cancellationToken);
                await _ShipmentRepository.Save(cancellationToken);

                return CreatedAtRoute("GetShipmentById", new { id = createShipment.Id }, createShipment);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Shipment>> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _ShipmentRepository.Delete(id, cancellationToken);
                await _ShipmentRepository.Save(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                _logger.LogError($"L'id {id} n\'existe pas");
                return StatusCode(StatusCodes.Status404NotFound, "L'Id n'a été trouvé ou à déjà supprimé");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Shipment>> Update(int id, [FromBody] ShipmentDTO Shipment, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingShipment = await _ShipmentRepository.GetByIdAsync(Shipment.Id, cancellationToken);
                if (existingShipment == null)
                {
                    return NotFound($"Shipment {Shipment.Id} not found");
                }

                existingShipment.Id = Shipment.Id;
                existingShipment.Date = DateTime.UtcNow; // a tester si la date de creation de maintenant ou aux choix 
                existingShipment.Address = Shipment.Address;
                existingShipment.City = Shipment.City;
                existingShipment.Region = Shipment.Region;
                existingShipment.Country = Shipment.Country;
                existingShipment.Code_Postal = Shipment.Code_Postal;

                await _ShipmentRepository.Update(existingShipment, cancellationToken);
                await _ShipmentRepository.Save(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                _logger.LogError($"L'id={id} / le Shipment={Shipment} n\'existe pas");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du Shipment");
            }
        }
    }
}
