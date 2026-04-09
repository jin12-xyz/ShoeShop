using Shop.Application.DTOs.Category;
using Shop.Models.Domain;



namespace Shop.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // Get all categories with product counts (admin list)
        Task<IEnumerable<Category>> GetCategoriesWithProductCountAsync();

        // Get single Category with its product
        Task<Category?> GetCategoryWithProductAsync(int id);

        // Get category by slug (URL friendly e.g. /category/mens-wear)
        Task<Category?> GetCategoryBySlugAsync(string slug);

        // Check if category name already exists (prevent duplicates)
        Task<bool> CategoryExistsAsync(string name);
        
    }
}
