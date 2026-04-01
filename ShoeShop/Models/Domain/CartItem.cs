namespace Shop.Models.Domain
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation
        public Cart Cart { get; set; } = null!;
        public ProductVariant ProductVariant { get; set; } = null!;
    }
}
