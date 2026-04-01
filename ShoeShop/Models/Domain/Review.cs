namespace Shop.Models.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        // Navigation
        public ApplicationUser User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
