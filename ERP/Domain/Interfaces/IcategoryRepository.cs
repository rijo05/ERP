using ERP.Domain.Entities;

namespace ERP.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<List<Category>> GetByNameAsync(string name);
    }
}
