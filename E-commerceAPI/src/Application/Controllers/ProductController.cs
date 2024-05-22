using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.src.Application.Controllers;

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

    [HttpGet("{id}", Name = "GetProductById")]
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
    public async Task<ActionResult<Product>> Create([FromBody] ProductDTO product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createProduct = new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Category_Id = product.Category_Id
        };

        await _productRepository.Create(createProduct, cancellationToken);
        await _productRepository.Save(cancellationToken);

        return CreatedAtRoute("GetProductById", new { id = createProduct.Id }, createProduct);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _productRepository.Delete(id, cancellationToken);
            await _productRepository.Save(cancellationToken);
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
    public async Task<ActionResult<Product>> Update(int id, [FromBody] ProductDTO product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.Id, cancellationToken);
            if (existingProduct == null)
            {
                return NotFound($"Product {product.Id} not found");
            }

            existingProduct.Id = product.Id;
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.Category_Id = product.Category_Id;

            await _productRepository.Update(existingProduct, cancellationToken);
            await _productRepository.Save(cancellationToken);

            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogError($"L'id={id} / le product={product} n\'existe pas");
            return StatusCode(500, "Une erreur est survenue lors de la mise à jour du product");
        }
    }
}
