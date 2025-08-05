using ERP.Domain.Entities;
using ERP.Persistence;

namespace ERP.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<List<Category>> GetByNameAsync(string name);

    public Task<Category?> GetByNameExactAsync(string name);
}
