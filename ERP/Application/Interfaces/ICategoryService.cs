using ERP.Application.DTOs.Category;
using ERP.Domain.Entities;

namespace ERP.Application.Interfaces;

public interface ICategoryService
{
    public Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
    public Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid id);
    public Task<List<CategoryResponseDTO>> GetCategoriesByNameAsync(string name);


    public Task<CategoryResponseDTO> AddCategoryAsync(CreateCategoryDTO categoryDTO);
    public Task DeleteCategoryAsync(Guid id);
    public Task<CategoryResponseDTO> UpdateCategoryAsync(Guid id, UpdateCategoryDTO categoryDTO);
}
