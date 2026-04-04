
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories;
using Shop.Infrastructure.Data;
using Shop.Models.Domain;

namespace Shop.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appContext) : base(appContext)
        {

        }
        public async Task<IEnumerable<Product>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(p => p.IsActive)
                .ToListAsync();
        }
        public async Task<Product?> GetByIdWithDetailAsync(int id)
        {
            return await _dbSet
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(e => e.Id == id);     
        }
        public async Task<IEnumerable<Product>> GetByCategoryAsync(int id)
        {
            return await _dbSet
                .Where(p => p.CategoryId == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByGenderAsync(string gender)
        {
            return await _dbSet
                .Where(c => c.Gender == gender)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> SearchAsync(string keyword)
        {
            return await _dbSet
                .Where(p =>
                p.Name.Contains(keyword) ||
                p.Brand != null && p.Brand.Contains(keyword)
                ).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet
                .Where(p => p.BasePrice >= minPrice &&
                p.BasePrice <= maxPrice).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllForAdminAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .ToListAsync();       
        }
        public async Task ToggleActive(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Product with id {id} was not found.");

            entity.IsActive = !entity.IsActive;
            await SaveChangesAsync();
        }
        public async Task<bool> SKUExistsAsync(string sku)
        {
            return await _dbSet
                .AnyAsync(e => e.SKU == sku);
        }
        public async Task<IEnumerable<Product>> GetLatestAsync(int count)
        {
            return await _dbSet
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}
