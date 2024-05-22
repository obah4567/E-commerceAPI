using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var getById = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (getById == null)
            {
                return null;
            }
            return getById;
        }

        public async Task Create(Category Category, CancellationToken cancellationToken)
        {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            var category = _context.Categories.Where(cat => cat.Id == id).FirstOrDefault();
            if (category == null)
            {
                throw new ArgumentException($"Cet {id} n'existe pas ou été supprimé");
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Category category, CancellationToken cancellationToken)
        {
            _context.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
