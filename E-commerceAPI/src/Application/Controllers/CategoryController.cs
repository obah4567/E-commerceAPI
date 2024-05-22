using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.src.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        private ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(_categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> GetAll(CancellationToken cancellationToken)
        {
            var Categorys = await _categoryRepository.GetAllAsync(cancellationToken);
            if (Categorys == null)
            {
                return NotFound();
            }
            return Ok(Categorys);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var Category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
                if (Category == null)
                {
                    return NotFound();
                }
                return Ok(Category);
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
        public async Task<ActionResult<Category>> Create([FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(categoryDto.Id, cancellationToken);
            if (existingCategory != null)
            {
                return Conflict(new { message = $"Category {categoryDto.Id} already exists" });
            }

            var createCategory = new Category()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name
            };

            await _categoryRepository.Create(createCategory, cancellationToken);
            await _categoryRepository.Save(cancellationToken);

            return CreatedAtRoute("GetCategoryById", new { id = createCategory.Id }, createCategory);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryRepository.Delete(id, cancellationToken);
                await _categoryRepository.Save(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                _logger.LogError($"L'id {id} n\'existe pas");
                return StatusCode(StatusCodes.Status404NotFound, $"L'{id} n'a été trouvé ou à déjà supprimé");
            }

        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Update(int id, [FromBody] CategoryDto category, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(category.Id, cancellationToken);
                if (existingCategory == null)
                {
                    return NotFound($"Category {category.Id} not found");
                }

                existingCategory.Name = category.Name;

                await _categoryRepository.Update(existingCategory, cancellationToken);
                await _categoryRepository.Save(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                _logger.LogError($"L'id={id} / le Category={category} n\'existe pas");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du Category");
            }
        }
    }
}
