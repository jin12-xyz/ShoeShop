using Shop.Application.DTOs.Category;

namespace Shop.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task CreateCategoryAsync(CreateCategoryDto dto);
        Task UpdateAsync(UpdateCategoryDto dto);
        Task<bool> CheckExistsAsync(string name);
        Task DeleteAsync(int id);
    }
}
