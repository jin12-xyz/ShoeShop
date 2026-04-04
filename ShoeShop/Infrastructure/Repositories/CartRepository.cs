using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories;
using Shop.Infrastructure.Data;
using Shop.Models.Domain;

namespace Shop.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext appContext) : base(appContext)
        {

        }
        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<IEnumerable<Cart?>> GetAllCartItemAsync(int cartId)
        {
            return await _dbSet
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                .Where(c => c.Id == cartId)
                .ToListAsync();
        }
        public async Task AddItemToCartAsync(int cartId, int productVariantId, int quantity)
        {
            // Check if cart is exists
            var cartEntity = await _dbContext.Carts
                 .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cartEntity == null)
                throw new KeyNotFoundException($"Entity with id {cartId} was not found.");

            // check if productVariant exists
            var variantEntity = await _dbContext.ProductVariants
                .FirstOrDefaultAsync(c => c.Id == productVariantId);

            if (variantEntity == null)
                throw new KeyNotFoundException($"Entity with id {productVariantId} was not found.");

            // check if variant already exists
            var existingItem = await _dbContext.CartItems
                .FirstOrDefaultAsync(c => c.CartId == cartId && c.VariantId == productVariantId);
                
            // check if exists
            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
                _dbContext.CartItems.Update(existingItem);
            }
            else // create cartitem if variant not exists
            {
                var cartItem = new CartItem
                {
                    CartId = cartId,
                    VariantId = productVariantId,
                    Quantity = quantity,
                    UnitPrice = variantEntity.Price
                };
                await _dbContext.CartItems.AddAsync(cartItem);
            }
            await SaveChangesAsync();
            
        }
        public async Task RemoveCartItemAsync(int itemId)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(c => c.Id == itemId);

            if (cartItem == null)
                throw new KeyNotFoundException($"Entity with id {itemId} was not found.");
            _dbContext.Remove(cartItem);
            await SaveChangesAsync();
        }
        public async Task UpdateItemQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem == null)
                throw new KeyNotFoundException($"Entity with id {cartItemId} was not found.");

            cartItem.Quantity = quantity;
            _dbContext.CartItems.Update(cartItem);

            await SaveChangesAsync();
        }
        public async Task ClearItemsAsync(int cartId)
        {
            var cartItems = await _dbContext.CartItems
                .Where(c => c.CartId == cartId)
                .ToListAsync();

            if (cartItems.Any())
                throw new KeyNotFoundException($"No items found for cart id {cartId}.");

            _dbContext.CartItems.RemoveRange(cartItems);
            await SaveChangesAsync();
        }
        public async Task<int> GetCartItemCountAsync(string userId)
        {
            var cart = await _dbSet
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return 0;

            return await _dbContext.CartItems
                .CountAsync(c => c.CartId == cart.Id);
        }
        public async Task<bool> CartItemExistsAsync(int cartId, int productVariantId)
        {

            return await _dbContext.CartItems
                .AnyAsync(c => c.CartId == cartId && c.VariantId == productVariantId);
        }
    }

}
