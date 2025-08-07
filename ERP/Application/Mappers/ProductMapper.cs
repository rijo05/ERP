using ERP.Application.DTOs.Product;
using ERP.Application.Services;
using ERP.Domain.Entities;
using System.Runtime.CompilerServices;

namespace ERP.Application.Mappers;

public class ProductMapper
{
    private readonly HateoasLinkService _hateoasLinkService;

    public ProductMapper(HateoasLinkService hateoasLinkService)
    {
        _hateoasLinkService = hateoasLinkService;
    }

    public ProductResponseDTO ToProductResponseDTO(Product product)
    {
        return new ProductResponseDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId.ToString(),
            IsActive = product.IsActive,
            Stock = product.Stock,
            MinimumStockLevel = product.MinimumStockLevel,
            Links = GenerateLinks(product)
        };
    }

    public List<ProductResponseDTO> ToProductResponseDTOList(List<Product> products)
    {
        return products.Select(x => ToProductResponseDTO(x)).ToList();
    }


    private Dictionary<string, object> GenerateLinks(Product product)
    {
        return _hateoasLinkService.GenerateLinksCRUD(
                    product.Id,
                    "products",
                    "GetById",
                    "Update",
                    "Delete"
        );
    }

}
