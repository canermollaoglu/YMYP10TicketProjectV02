using System.Linq.Expressions;
using TicketApp.Core.Entities;

namespace TicketApp.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //Asenkron Programlama Nedir?
        //Asenkron programlama, bir işlemin tamamlanmasını beklemeden diğer işlemlere devam etmeyi sağlayan bir programlama modelidir. Bu, özellikle I/O işlemleri gibi zaman alıcı görevlerde performansı artırmak için kullanılır. Asenkron programlama, genellikle "async" ve "await" anahtar kelimeleri kullanılarak gerçekleştirilir.
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}