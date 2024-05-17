using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.src.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private IWishListRepository _WishListRepository;
        private ILogger<WishListController> _logger;

        public WishListController(IWishListRepository WishListRepository, ILogger<WishListController> logger)
        {
            _WishListRepository = WishListRepository ?? throw new ArgumentNullException(nameof(_WishListRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishList>> GetAll(CancellationToken cancellationToken)
        {
            var WishLists = await _WishListRepository.GetAllAsync(cancellationToken);
            if (WishLists == null)
            {
                return NotFound();
            }
            return Ok(WishLists);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishList>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var WishList = await _WishListRepository.GetByIdAsync(id, cancellationToken);
                if (WishList == null)
                {
                    return NotFound();
                }
                return Ok(WishList);
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
        public async Task<ActionResult<WishList>> Create([FromRoute] WishListDTO WishList, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == true)
            {
                var createWishList = new WishList()
                {
                    Id = WishList.Id,
                    Product_Id = WishList.Product_Id,
                    Product = WishList.Product
                };

                await _WishListRepository.Create(createWishList, cancellationToken);
                await _WishListRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = WishList.Id }, createWishList);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishList>> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _WishListRepository.Delete(id, cancellationToken);
                await _WishListRepository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                _logger.LogError($"L'id {id} n\'existe pas");
                return StatusCode(StatusCodes.Status404NotFound, $"L'Id {id} ne peut être supprimer, il n'existe pas");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WishList>> Update(int id, [FromRoute] WishListDTO WishList, CancellationToken cancellationToken)
        {
            var update = await _WishListRepository.GetByIdAsync(id, cancellationToken);
            if (update == null)
            {
                return NotFound();
            }

            if (WishList == null)
            {
                return BadRequest("Le WishList n'est peut pas être null");
            }

            try
            {
                var createWishList = new WishList()
                {
                    Id = WishList.Id,
                    Product_Id = WishList.Product_Id,
                    Product = WishList.Product
                };

                await _WishListRepository.Update(id, createWishList, cancellationToken);
                await _WishListRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = WishList.Id }, createWishList);
                //return CreatedAtRoute(nameof(Create), new { id = WishList.Id }, createWishList);
            }
            catch (Exception)
            {
                _logger.LogError($"L'id={id} / le WishList={WishList} n\'existe pas");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du WishList");
            }
        }
    }
}
