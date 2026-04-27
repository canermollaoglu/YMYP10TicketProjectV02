using Microsoft.EntityFrameworkCore;
using TicketApp.Core.Entities;
using TicketApp.Core.Interfaces;
using TicketApp.DataAccess.Context;

namespace TicketApp.DataAccess.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Product?> GetByIdWithCategoryAsync(Guid id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync());
        }

        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}