namespace Shop.Models.Domain
{
    public class Order
    {
        // Primary Key
        public int Id { get; set; }

        // Foreign Keys
        public string UserId { get; set; } = string.Empty;
        public int AddressId { get; set; }

        // Fields
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }

        // Navigation
        public ApplicationUser User { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
    }
}
