using ERP.Application.DTOs.Category;
using ERP.Application.DTOs.Product;
using ERP.Application.Services;
using ERP.Domain.Entities;

namespace ERP.Application.Mappers
{
    public class CategoryMapper
    {
        private readonly HateoasLinkService _hateoasLinkService;

        public CategoryMapper(HateoasLinkService hateoasLinkService)
        {
            _hateoasLinkService = hateoasLinkService;
        }

        public CategoryResponseDTO ToCategoryResponseDTO(Category category)
        {
            return new CategoryResponseDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive,
                Links = GenerateLinks(category)
            };
        }

        public List<CategoryResponseDTO> ToCategoryResponseDTOList(List<Category> categories)
        {
            return categories.Select(x => ToCategoryResponseDTO(x)).ToList();
        }


        private Dictionary<string, object> GenerateLinks(Category category)
        {
            return _hateoasLinkService.GenerateLinks(
                        category.Id,
                        "categories",
                        "GetById",
                        "Update",
                        "Delete"
            );
        }

    }
}
