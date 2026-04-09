using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Category;
using Shop.Application.Interfaces.Services;
using Shop.Web.ViewModels.Category;

namespace Shop.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryRepository, IMapper mapper)
        {
            this._categoryService = categoryRepository;
            this._mapper = mapper;
        }
        // Index list all category
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryFormViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check for duplicate name
            if(await _categoryService.CheckExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Categry Name already exists.");
                return View(model);
            }

            // Map category veiw model to dto
            var dto = _mapper.Map<CreateCategoryDto>(model);
            await _categoryService.CreateCategoryAsync(dto);

            TempData["Success"] = "Category created successfull.";
            return RedirectToAction(nameof(Index));
        }

        // Edit - Show Form
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var viewModel = _mapper.Map<CategoryFormViewModel>(category);
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = _mapper.Map<UpdateCategoryDto>(model);
            await _categoryService.UpdateAsync(dto);

            TempData["Success"] = "Category update successfully.";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var viewModel = _mapper.Map<CategoryViewModel>(category);
            return View(viewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            TempData["Success"] = "Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
