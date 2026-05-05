using System.Linq.Expressions;
using TicketApp.Core.Entities;

namespace TicketApp.Core.Interfaces
{
    public interface IService<TResponseDTO, TUpdateDTO, TCreateDTO>
    {
        Task<IEnumerable<TResponseDTO>> GetAllAsync();
        Task<TResponseDTO?> GetByIdAsync(Guid id);
        Task<TResponseDTO> AddAsync(TCreateDTO entity);
        Task UpdateAsync(Guid id, TUpdateDTO entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TResponseDTO>> FindAsync(Expression<Func<Product, bool>> predicate);
    }
}