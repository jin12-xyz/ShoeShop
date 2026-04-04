using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories;
using Shop.Infrastructure.Data;
using Shop.Models.Domain;

namespace Shop.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext appContext) : base(appContext)
        {

        }

        public async Task<IEnumerable<Address>> GetByUserAsync(string userId)
        {
             return await _dbSet
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
        public async Task<Address?> GetByIdAndUserAsync(int addressId, string userId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId);
        }
        public async Task<Address?> GetDefaultByUserAsync(string userId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IsDefault && a.UserId == userId);
        }
        public async Task ClearDefaultAsync(string userId)
        {
            var addresses = await _dbSet
                .Where(a => a.IsDefault && a.UserId == userId)
                .ToListAsync();

            if (!addresses.Any())
                return;

            foreach(var address in addresses)
            {
                address.IsDefault = false;
            }

            await SaveChangesAsync();
        }
        public async Task<IEnumerable<Address>> GetWithOrdersByUserAsync(string userId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .Include(o => o.Orders)
                .ToListAsync();
        }
    }
}
