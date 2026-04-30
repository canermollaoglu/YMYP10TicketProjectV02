using System.Linq.Expressions;
using FluentValidation;
using TicketApp.Business.Validators;
using TicketApp.Core.Entities;
using TicketApp.Core.Interfaces;
using TicketApp.DataAccess.Context;
using TicketApp.DataAccess.Repositories;

namespace TicketApp.Business.Services
{
    public class ProductService : IService<Product>
    {
        private readonly ProductRepository _repository;
        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Product entity)
        {
            var validator = new ProductValidator();
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }

        public Task UpdateAsync(Product entity)
        {
            var validator = new ProductValidator();
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            return _repository.UpdateAsync(entity);
        }
    }
}