using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models.Domain;

namespace Shop.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addressess { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // for identity table

            // Product table config
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

                entity.Property(p => p.BasePrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

                entity.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(2000);

                entity.Property(p => p.Brand)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(p => p.CreatedAt)
                .ValueGeneratedNever();

                entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.Reviews)
                .WithOne(c => c.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
               
            });

            // Category table config
            builder.Entity<Category>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(2000);
            });

            // Product variant 
            builder.Entity<ProductVariant>(entity =>
            {
                entity.Property(p => p.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

                entity.HasKey(p => p.Id);
                entity.Property(p => p.Size)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(50);

                entity.HasOne(p => p.Product)
                .WithMany(c => c.ProductVariants)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            });

            // Product image
            builder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.ProductURL)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasOne(p => p.Product)
                .WithMany(c => c.ProductImages)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // Cart 
            builder.Entity<Cart>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.CreatedAt)
                .ValueGeneratedNever();

                entity.HasOne(p => p.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            });

            // CartITem
            builder.Entity<CartItem>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

                entity.HasOne(p => p.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(p => p.CartId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.ProductVariant)
                .WithMany(c => c.CartItems)
                .HasForeignKey(p => p.VariantId)
                .OnDelete(DeleteBehavior.Cascade);
                
            });

            // Oder
            builder.Entity<Order>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

                entity.Property(p => p.Status)
                .HasMaxLength(20)
                .IsRequired();

                entity.Property(p => p.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(p => p.OrderDate)
                .ValueGeneratedNever();

                entity.HasOne(p => p.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Address)
                .WithMany(c => c.Orders)
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Order item
            builder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

                entity.HasOne(p => p.Order)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete items when order deleted

                entity.HasOne(p => p.ProductVariant)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(p => p.VariantId)
                .OnDelete(DeleteBehavior.Restrict); // keep order history if variant deleted
            });

            // Address
            builder.Entity<Address>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FullName).HasMaxLength(100).IsRequired();
                entity.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired();
                entity.Property(p => p.HouseNo).HasMaxLength(20);
                entity.Property(p => p.BlockLot).HasMaxLength(50);
                entity.Property(p => p.Phase).HasMaxLength(50);
                entity.Property(p => p.Street).HasMaxLength(100);
                entity.Property(p => p.Barangay).HasMaxLength(100).IsRequired();
                entity.Property(p => p.City).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Province).HasMaxLength(100).IsRequired();
                entity.Property(p => p.ZipCode).HasMaxLength(10).IsRequired();
                entity.Property(p => p.Country).HasMaxLength(100).IsRequired();

                // Prevent using duplicate Address
                entity.HasIndex(p => new 
                {
                    p.UserId,
                    p.HouseNo,
                    p.BlockLot,
                    p.Phase,
                    p.Barangay,
                    p.City,
                    p.ZipCode
                }).IsUnique();

                entity.HasOne(p => p.User)
                .WithMany(c => c.Addresses)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // Review
            builder.Entity<Review>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Comment).HasMaxLength(1000);

                entity.HasOne(p => p.User)
                .WithMany(c => c.Reviews)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Product)
                .WithMany(c => c.Reviews)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
