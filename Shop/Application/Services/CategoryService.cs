using AutoMapper;
using Shop.Application.DTOs.Category;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Interfaces.Services;
using Shop.Models.Domain;

namespace Shop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            this._categoryRepo = categoryRepo;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetCategoriesWithProductCountAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with id {id} was not found");

            var dto = _mapper.Map<CategoryDto>(category);
            return dto;
        }
        public async Task CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            category.Slug = dto.Name.ToLower().Replace(" ", "-");

            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var existing = await _categoryRepo.GetByIdAsync(dto.Id);
            if (existing == null) throw new KeyNotFoundException();

            _mapper.Map(dto, existing); // updates properties
            existing.Slug = GenerateSlug(dto.Name);

            _categoryRepo.Update(existing);

            await _categoryRepo.SaveChangesAsync();
        }
        public async Task<bool> CheckExistsAsync(string name)
        {
          return await _categoryRepo.CategoryExistsAsync(name);
            
        }
        public async Task DeleteAsync(int id)
        {
            await _categoryRepo.DeleteAsync(id);
            await _categoryRepo.SaveChangesAsync();
        }
        private string GenerateSlug(string name)
        {
            return name.Trim().ToLower().Replace(" ", "-");
        }
    }
}
