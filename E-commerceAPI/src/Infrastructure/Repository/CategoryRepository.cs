using E_commerceAPI.src.Domain.DTO;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;

namespace E_commerceAPI.src.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            var category = _context.Categories.ToList();
            if (category == null)
            {
                return null;
            }
            return Task.FromResult(category);
        }

        public Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var category = _context.Categories.Where(cat => cat.Id == id).FirstOrDefault();
            if (category == null)
            {
                return null;
            }
            return Task.FromResult(category);
        }
        public Task Create(Category Category, CancellationToken cancellationToken)
        {
            _context.Categories.Add(Category);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var category = _context.Categories.Where(cat => cat.Id == id).FirstOrDefault();
            if (category == null)
            {
                return null;
            }
            _context.Remove(category);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Save()
        {
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Update(int id, Category category, CancellationToken cancellationToken)
        {
            var cat = _context.Categories.Where(cat => cat.Id == id).FirstOrDefault();
            if (cat == null)
            {
                return null;
            }

            var updateCategory = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name
                //a revoir s'il ne faut pas mettre à jour la liste des produits
            };

            _context.Add(updateCategory);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
