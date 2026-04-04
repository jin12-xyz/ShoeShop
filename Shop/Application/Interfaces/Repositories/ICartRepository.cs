using Shop.Application.DTOs.CartItem;
using Shop.Infrastructure.Data;
using Shop.Models.Domain;

namespace Shop.Application.Interfaces.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        // Get cart by userId (foreign key)
        Task<Cart?> GetCartByUserIdAsync(string userId);

        // Get cart with all items and variant details (cart page)
        Task<IEnumerable<Cart?>> GetAllCartItemAsync(int cartId);

        // Add item to cart
        Task AddItemToCartAsync(int cartId, int productVariantId, int quantity);

        // Remove item from cart
        Task RemoveCartItemAsync(int itemId);

        // Update item quantity
        Task UpdateItemQuantityAsync(int cartItemId, int quantity);

        // Clear all items from cart (after checkout)
        Task ClearItemsAsync(int cartId);

        // Get total item count (navbar badge)
        Task<int> GetCartItemCountAsync(string userId);

        // Check if variant already exists in cart
        Task<bool> CartItemExistsAsync(int cartId, int productVariantId);
    }
}
