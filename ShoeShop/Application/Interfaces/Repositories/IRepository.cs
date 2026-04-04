namespace Shop.Application.Interfaces.Repositories
{
    // T represents the entity type
    public interface IRepository<T> where T : class
    {
        // Retrieve all records of T 
        Task<IEnumerable<T>> GetAllAsync();

        // Retrieve a single record by primary key
        Task<T?> GetByIdAsync(int id);

        // Add new record to the database
        Task<T> AddAsync(T entity);

        // Update the existing record
        void Update(T entity);

        // Delete record by primary key 
        Task DeleteAsync(int id);

        // Check if record exist by primary key
        Task<bool> ExistsAsync(int id);

        // Save all pending to the database
        Task<bool> SaveChangesAsync();
    }
}
