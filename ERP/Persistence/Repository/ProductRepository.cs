using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERP.Persistence.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Product>> GetByNameAsync(string name)
        {
            return await _appDbContext.Products
                .Where(u => EF.Functions.Like(u.Name, $"%{name}%"))
                .ToListAsync();
        }
    }
}
