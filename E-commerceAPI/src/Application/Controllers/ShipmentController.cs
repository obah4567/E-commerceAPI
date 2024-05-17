using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
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

        [HttpGet("{id}")]
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
        public async Task<ActionResult<Shipment>> Create([FromRoute] ShipmentDTO Shipment, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == true)
            {
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
                await _ShipmentRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = Shipment.Id }, createShipment);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Shipment>> Delete(int id, CancellationToken cancellationToken)
        {
            await _ShipmentRepository.Delete(id, cancellationToken);
            await _ShipmentRepository.Save();

            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Shipment>> Update(int id, [FromRoute] ShipmentDTO Shipment, CancellationToken cancellationToken)
        {
            var update = await _ShipmentRepository.GetByIdAsync(id, cancellationToken);
            if (update == null)
            {
                return NotFound();
            }

            if (Shipment == null)
            {
                return BadRequest("Le Shipment n'est peut pas être null");
            }

            try
            {
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

                await _ShipmentRepository.Update(id, createShipment, cancellationToken);
                await _ShipmentRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = Shipment.Id }, createShipment);
                //return CreatedAtRoute(nameof(Create), new { id = Shipment.Id }, createShipment);
            }
            catch (Exception)
            {
                _logger.LogError($"L'id={id} / le Shipment={Shipment} n\'existe pas");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du Shipment");
            }
        }
    }
}
