using ERP.Application.DTOs.Product;
using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace ERP.Persistence.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext) {}

        public async Task<List<Product>> GetByCategoryIdAsync(Guid categoryId)
        {
            return await _appDbContext.Products
                            .Where(p => p.CategoryId == categoryId)
                            .ToListAsync();
        }

        public async Task<List<Product>> GetByNameAsync(string name)
        {
            return await _appDbContext.Products
                            .Where(u => EF.Functions.Like(u.Name, $"%{name}%"))
                            .ToListAsync();
        }
        public async Task<List<Product>> GetFilteredAsync(ProductFilterDTO dto)
        {
            var query = _appDbContext.Products.AsQueryable();

            if (dto.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == dto.CategoryId.Value);

            if (!string.IsNullOrWhiteSpace(dto.Name))
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{dto.Name}%"));

            if (dto.MinPrice.HasValue)
                query = query.Where(p => p.Price >= dto.MinPrice.Value);

            if (dto.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= dto.MaxPrice.Value);

            if (dto.IsActive.HasValue)
                query = query.Where(p => p.IsActive == dto.IsActive.Value);

            query = dto.Sort switch
            {
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };

            var page = Math.Max(dto.Page, 1);
            var pageSize = Math.Clamp(dto.PageSize, 1, 100);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
