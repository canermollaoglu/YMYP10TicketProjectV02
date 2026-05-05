using TicketApp.Core.DTOs;
using TicketApp.Core.Entities;

namespace TicketApp.Core.Interfaces
{
    public interface IProductService : IService<ProductResponseDTO, ProductUpdateDTO, ProductCreateDTO>
    {
        Task<ProductResponseDTO?> GetByIdWithCategoryAsync(Guid id);
        Task<IEnumerable<ProductResponseDTO>> GetByCategoryIdAsync(Guid categoryId);
        Task<IEnumerable<ProductResponseDTO>> GetProductsWithCategoryAsync();
        Task<IEnumerable<ProductResponseDTO>> GetByPriceRange(decimal minPrice, decimal maxPrice);
    }
}