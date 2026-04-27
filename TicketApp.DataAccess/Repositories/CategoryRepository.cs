using TicketApp.Core.Entities;
using TicketApp.DataAccess.Context;
using TicketApp.DataAccess.Repositories;

namespace TicketApp.DataAcess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}