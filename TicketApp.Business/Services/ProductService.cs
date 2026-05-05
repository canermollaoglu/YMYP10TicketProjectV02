using System.Linq.Expressions;
using FluentValidation;
using TicketApp.Business.Validators;
using TicketApp.Core.DTOs;
using TicketApp.Core.Entities;
using TicketApp.Core.Interfaces;
using TicketApp.DataAccess.Context;
using TicketApp.DataAccess.Repositories;

namespace TicketApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;
        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<ProductResponseDTO> AddAsync(ProductCreateDTO entity)
        {
            //DTO'yu Entity'e dönüştür
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                StockQuantity = entity.StockQuantity
            };

            var validator = new ProductValidator();
            var result = validator.Validate(product);

            if (!result.IsValid)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            await _repository.AddAsync(product);

            return new ProductResponseDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                StockQuantity = product.StockQuantity
            };

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

        public async Task<IEnumerable<ProductResponseDTO>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            var products = await _repository.FindAsync(predicate);
            return products.Select(p => new ProductResponseDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                StockQuantity = p.StockQuantity
            });
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductResponseDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                StockQuantity = p.StockQuantity
            });
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByCategoryIdAsync(Guid categoryId)
        {
            var products = await _repository.GetByCategoryIdAsync(categoryId);

            return products.Select(p => new ProductResponseDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                StockQuantity = p.StockQuantity
            });
        }

        public async Task<ProductResponseDTO?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            var productResponse = new ProductResponseDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                StockQuantity = product.StockQuantity
            };
            return productResponse;
        }

        public async Task<ProductResponseDTO?> GetByIdWithCategoryAsync(Guid id)
        {
            var product = await _repository.GetByIdWithCategoryAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            var productResponse = new ProductResponseDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                StockQuantity = product.StockQuantity
            };
            return productResponse;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var products = await _repository.GetByPriceRange(minPrice, maxPrice);
            return products.Select(p => new ProductResponseDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                StockQuantity = p.StockQuantity
            });

        }

        public async Task<IEnumerable<ProductResponseDTO>> GetProductsWithCategoryAsync()
        {
            var products = await _repository.GetProductsWithCategoryAsync();
            return products.Select(p => new ProductResponseDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                StockQuantity = p.StockQuantity,
                CategoryName = p.Category != null ? p.Category.Name : null
            });
        }

        public async Task UpdateAsync(Guid id, ProductUpdateDTO entity)
        {
            var existingProduct = await _repository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            existingProduct.Name = entity.Name;
            existingProduct.Description = entity.Description;
            existingProduct.Price = entity.Price;
            existingProduct.CategoryId = entity.CategoryId;
            existingProduct.StockQuantity = entity.StockQuantity;

            var validator = new ProductValidator();
            var result = validator.Validate(existingProduct);

            if (!result.IsValid)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            await _repository.UpdateAsync(existingProduct);
        }
    }
}