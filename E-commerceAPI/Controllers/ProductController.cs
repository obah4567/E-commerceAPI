using E_commerceAPI.DTO;
using E_commerceAPI.Models;
using E_commerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductRepository _productRepository;
    private ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(_productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        if (products == null)
        {
            return NotFound();
        }
        return Ok(products);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellationToken) 
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
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
    public async Task<ActionResult<Product>> Create([FromRoute] ProductDTO product, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == true)
        {
            var createProduct = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category_Id = product.Category_Id
            };

            await _productRepository.Create(createProduct, cancellationToken);
            await _productRepository.Save();

            return CreatedAtRoute(nameof(Create), new { id = product.Id }, createProduct);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> Delete(int id, CancellationToken cancellationToken)
    {
        await _productRepository.Delete(id, cancellationToken);
        await _productRepository.Save();

        return NoContent();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Update(int id, [FromRoute] ProductDTO product, CancellationToken cancellationToken)
    {
        var update = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (update == null)
        {
            return NotFound();
        }

        if (product == null)
        {
            return BadRequest("Le product n'est peut pas être null");
        }

        try
        {
            var createProduct = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category_Id = product.Category_Id
            };

            await _productRepository.Update(id, createProduct, cancellationToken);
            await _productRepository.Save();

            return CreatedAtRoute(nameof(Create), new { id = product.Id }, createProduct);
            //return CreatedAtRoute(nameof(Create), new { id = product.Id }, createProduct);
        }
        catch (Exception)
        {
            _logger.LogError($"L'id={id} / le product={product} n\'existe pas");
            return StatusCode(500, "Une erreur est survenue lors de la mise à jour du product");
        }
    }
}
