using Shop.Models.Domain;

namespace Shop.Application.Interfaces.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        // Get all addresses for a specific user
        Task<IEnumerable<Address>> GetByUserAsync(string userId);

        // Get a specific address ensuring it belongs to the user (security)
        Task<Address?> GetByIdAndUserAsync(int addressId, string userId);

        // Get the default address of the user (for checkout)
        Task<Address?> GetDefaultByUserAsync(string userId);

        // Remove default flag from all addresses of a user
        // (call before setting a new default)
        Task ClearDefaultAsync(string userId);

        // Get addresses with related orders (for history views)
        Task<IEnumerable<Address>> GetWithOrdersByUserAsync(string userId);
    }
}
