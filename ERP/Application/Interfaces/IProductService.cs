using ERP.Application.DTOs.Product;
using ERP.Domain.Entities;
using ERP.Domain.ValueObjects;

namespace ERP.Application.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductResponseDTO>> GetAllProductsAsync();
        public Task<ProductResponseDTO?> GetProductByIdAsync(Guid id);
        public Task<List<ProductResponseDTO>> GetProductsByNameAsync(string name);
        Task<List<ProductResponseDTO>> GetFilteredProductsAsync(ProductFilterDTO productFilterDTO);

        public Task<ProductResponseDTO> AddProductAsync(CreateProductDTO product);
        public Task DeleteProductAsync(Guid id);
        public Task<ProductResponseDTO> UpdateProductAsync(Guid id, UpdateProductDTO updatedProduct);
        public Task<ProductResponseDTO> UpdateInventory(Guid id, UpdateProductInventoryDTO inventoryDTO);
    }
}
