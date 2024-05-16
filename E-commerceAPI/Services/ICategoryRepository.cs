﻿using E_commerceAPI.Models;

namespace E_commerceAPI.Services
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task Create(Category Category, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Save();
        Task Update(int id, Category Category, CancellationToken cancellationToken);
    }
}
