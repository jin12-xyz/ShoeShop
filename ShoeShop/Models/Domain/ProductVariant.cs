using System.Drawing;

namespace Shop.Models.Domain
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string SKU { get; set; } = string.Empty;

        public Product Product { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
