using TicketApp.Core.Entities;

namespace TicketApp.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetByIdWithCategoryAsync(Guid id);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
        Task<IEnumerable<Product>> GetByPriceRange(decimal minPrice, decimal maxPrice);
    }
}