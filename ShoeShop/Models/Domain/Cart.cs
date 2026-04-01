namespace Shop.Models.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ApplicationUser User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
