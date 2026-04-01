namespace Shop.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public Category Category { get; set; } = null!;
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
