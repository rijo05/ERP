using ERP.Application.DTOs.Product;
using ERP.Domain.Entities;

namespace ERP.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<List<Product>> GetByNameAsync(string name);
        public Task<List<Product>> GetByCategoryIdAsync(Guid categoryId);

        public Task<List<Product>> GetFilteredAsync(ProductFilterDTO productFilterDTO);       
    }
}
