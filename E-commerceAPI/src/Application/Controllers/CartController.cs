using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.src.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository _CartRepository;
        private ILogger<CartController> _logger;

        public CartController(ICartRepository CartRepository, ILogger<CartController> logger)
        {
            _CartRepository = CartRepository ?? throw new ArgumentNullException(nameof(_CartRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cart>> GetAll(CancellationToken cancellationToken)
        {
            var Carts = await _CartRepository.GetAllAsync(cancellationToken);
            if (Carts == null)
            {
                return NotFound();
            }
            return Ok(Carts);
        }

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cart>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var Cart = await _CartRepository.GetByIdAsync(id, cancellationToken);
                if (Cart == null)
                {
                    return NotFound();
                }
                return Ok(Cart);
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Cart>> Create([FromBody] CartDTO Cart, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCart = await _CartRepository.GetByIdAsync(Cart.Id, cancellationToken);
            if (existingCart != null)
            {
                return Conflict(new { message = $"Cart {Cart.Id} already exists" });
            }

            var createCart = new Cart()
            {
                Id = Cart.Id,
                Quantity = Cart.Quantity,
                Product_Id = Cart.Product_Id
            };

            await _CartRepository.Create(createCart, cancellationToken);
            await _CartRepository.Save(cancellationToken);

            return CreatedAtRoute("GetById", new { id = createCart.Id }, createCart);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cart>> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _CartRepository.Delete(id, cancellationToken);
                await _CartRepository.Save(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                _logger.LogError($"L'id {id} n\'existe pas");
                return StatusCode(StatusCodes.Status404NotFound, "L'Id n'a été trouvé ou à déjà supprimé");
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cart>> Update(int id, [FromBody] CartDTO Cart, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existingCart = await _CartRepository.GetByIdAsync(Cart.Id, cancellationToken);
                if (existingCart == null)
                {
                    return NotFound($"Cart {Cart.Id} not found");
                }

                existingCart.Quantity = Cart.Quantity;
                existingCart.Product_Id = Cart.Product_Id;

                await _CartRepository.Update(existingCart, cancellationToken);
                await _CartRepository.Save(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                _logger.LogError($"L'id={id} / le Cart={Cart} n\'existe pas");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du Cart");
            }
        }
    }
}

