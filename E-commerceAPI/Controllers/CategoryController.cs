using E_commerceAPI.DTO;
using E_commerceAPI.Models;
using E_commerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Controllers
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

        [HttpGet("{id}")]
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
        public async Task<ActionResult<Category>> Create([FromRoute] CategoryDto category, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == true)
            {
                var createCategory = new Category()
                {
                    Id = category.Id,
                    Name = category.Name
                };

                await _categoryRepository.Create(createCategory, cancellationToken);
                await _categoryRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = category.Id }, createCategory);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryRepository.Delete(id, cancellationToken);
            await _categoryRepository.Save();

            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Update(int id, [FromRoute] CategoryDto category, CancellationToken cancellationToken)
        {
            var update = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (update == null)
            {
                return NotFound();
            }

            if (category == null)
            {
                return BadRequest("Le Category n'est peut pas être null");
            }

            try
            {
                var createCategory = new Category()
                {
                    Id = category.Id,
                    Name = category.Name,
                };

                await _categoryRepository.Update(id, createCategory, cancellationToken);
                await _categoryRepository.Save();

                return CreatedAtRoute(nameof(Create), new { id = category.Id }, createCategory);
                //return CreatedAtRoute(nameof(Create), new { id = Category.Id }, createCategory);
            }
            catch (Exception)
            {
                _logger.LogError($"L'id={id} / le Category={category} n\'existe pas");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du Category");
            }
        }
    }
}
