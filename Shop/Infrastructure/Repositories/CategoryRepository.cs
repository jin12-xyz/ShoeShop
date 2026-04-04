using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs.Category;
using Shop.Application.Interfaces.Repositories;
using Shop.Infrastructure.Data;
using Shop.Models.Domain;

namespace Shop.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appContext) : base(appContext)
        {
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesWithProductCountAsync()
        {        
            return await _dbSet
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    ProductCount = c.Products.Count()
                })
                .ToListAsync();
        }
        public async Task<Category?> GetCategoryWithProductAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == id); 
        }
        public async Task<Category?> GetCategoryBySlugAsync(string slug)
        {
            return await _dbSet.FirstOrDefaultAsync( p => p.Slug == slug);
        }
        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await _dbSet
                .AnyAsync(p => p.Name == name);
        }
    }
}
