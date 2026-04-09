// Web/ViewModels/Category/CategoryFormViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.ViewModels.Category
{
    // Used for Create and Edit forms
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}