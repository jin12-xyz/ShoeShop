namespace Shop.Web.ViewModels.CategoryVM
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ProductCount { get; set; }
    }
}
