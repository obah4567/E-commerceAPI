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

        [HttpGet("{id}")]
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
        public async Task<ActionResult<Cart>> Create([FromRoute] CartDTO Cart, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == true)
            {
                var createCart = new Cart()
                {
                    Id = Cart.Id,
                    Quantity = Cart.Quantity,
                    Product_Id = Cart.Product_Id
                };

                await _CartRepository.Create(createCart, cancellationToken);
                await _CartRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = Cart.Id }, createCart);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cart>> Delete(int id, CancellationToken cancellationToken)
        {
            await _CartRepository.Delete(id, cancellationToken);
            await _CartRepository.Save();

            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cart>> Update(int id, [FromRoute] CartDTO Cart, CancellationToken cancellationToken)
        {
            var update = await _CartRepository.GetByIdAsync(id, cancellationToken);
            if (update == null)
            {
                return NotFound();
            }

            if (Cart == null)
            {
                return BadRequest("Le Cart n'est peut pas être null");
            }

            try
            {
                var createCart = new Cart()
                {
                    Id = Cart.Id,
                    Quantity = Cart.Quantity,
                    Product_Id = Cart.Product_Id
                };

                await _CartRepository.Update(id, createCart, cancellationToken);
                await _CartRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = Cart.Id }, createCart);
                //return CreatedAtRoute(nameof(Create), new { id = Cart.Id }, createCart);
            }
            catch (Exception)
            {
                _logger.LogError($"L'id={id} / le Cart={Cart} n\'existe pas");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du Cart");
            }
        }
    }
}

