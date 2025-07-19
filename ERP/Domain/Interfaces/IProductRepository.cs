using ERP.Domain.Entities;

namespace ERP.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<List<Product>> GetByNameAsync(string name);
    }
}
