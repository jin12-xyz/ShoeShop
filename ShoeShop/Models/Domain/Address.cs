namespace Shop.Models.Domain
{
    public class Address
    {
        //primary
        public int Id { get; set; }

        // Foreign-key
        public string UserId { get; set; } = string.Empty;

        // Fields
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // Detailed address fields
        public string HouseNo { get; set; } = string.Empty;      // House/Lot/Unit No.
        public string BlockLot { get; set; } = string.Empty;     // Block & Lot
        public string Phase { get; set; } = string.Empty;        // Phase
        public string Street { get; set; } = string.Empty;       // Street Name
        public string Barangay { get; set; } = string.Empty;     // Barangay
        public string City { get; set; } = string.Empty;         // City/Municipality
        public string Province { get; set; } = string.Empty;     // Province/State
        public string ZipCode { get; set; } = string.Empty;      // Zip Code
        public string Country { get; set; } = string.Empty;      // Country
        public bool IsDefault { get; set; }
        
        //navigation
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
