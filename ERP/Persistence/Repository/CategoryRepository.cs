using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERP.Persistence.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Category>> GetByNameAsync(string name)
        {
            return await _appDbContext.Categories
                .Where(u => EF.Functions.Like(u.Name, $"%{name}%"))
                .ToListAsync();
        }
    }
}
