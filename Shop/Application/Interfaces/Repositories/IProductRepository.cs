using Shop.Models.Domain;

namespace Shop.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        // Get all active product for cutomer
        Task<IEnumerable<Product>> GetAllActiveAsync();

        // Get single product with variants and image (product detail page)
        Task<Product?> GetByIdWithDetailAsync(int id);

        // Get product by category
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

        // Get products by gender filter
        Task<IEnumerable<Product>> GetByGenderAsync(string gender);

        // Search product by name or brand
        Task<IEnumerable<Product>> SearchAsync(string keyword);

        // Get product by price range
        Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        // For Admin Access - Get all product including inactive
        Task<IEnumerable<Product>> GetAllForAdminAsync();

        // Toggle active - Inactive status
        Task ToggleActive(int id);

        // Check if SKU already exists (prevent duplicates)
        Task<bool> SKUExistsAsync(string sku);

        // Get featured/lates product (home page)
        Task<IEnumerable<Product>> GetLatestAsync(int count);
    }
}
