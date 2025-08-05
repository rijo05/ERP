using ERP.Application.DTOs.Category;
using ERP.Application.DTOs.User;
using ERP.Application.Interfaces;
using ERP.Application.Mappers;
using ERP.Application.Validators.ProductValidator;
using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using ERP.Persistence.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ERP.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly CategoryMapper _categoryMapper;
        private readonly IValidator<CreateCategoryDTO> _validatorCreate;
        private readonly IValidator<UpdateCategoryDTO> _validatorUpdate;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, CategoryMapper categoryMapper, IValidator<CreateCategoryDTO> validatorCreate, IValidator<UpdateCategoryDTO> validatorUpdate, IUnitOfWork unitOfWork) 
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _categoryMapper = categoryMapper;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate; 
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _categoryMapper.ToCategoryResponseDTOList(categories);
        }

        public async Task<List<CategoryResponseDTO>> GetCategoriesByNameAsync(string name)
        {
            var categories = await _categoryRepository.GetByNameAsync(name);
            return _categoryMapper.ToCategoryResponseDTOList(categories);
        }

        public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category is null ? null : _categoryMapper.ToCategoryResponseDTO(category);
        }

        public async Task<CategoryResponseDTO> AddCategoryAsync(CreateCategoryDTO categoryDTO)
        {
            //Valida os dados
            var validationResult = await _validatorCreate.ValidateAsync(categoryDTO);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            //Ve se o nome da categoria já existe
            if (await _categoryRepository.GetByNameExactAsync(categoryDTO.Name) is not null)
                throw new ValidationException($"Category name '{categoryDTO.Name}' already exists.");



            var category = new Category(categoryDTO.Name, categoryDTO.Description);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return _categoryMapper.ToCategoryResponseDTO(category);
            
        }
        public async Task<CategoryResponseDTO> UpdateCategoryAsync(Guid id, UpdateCategoryDTO categoryDTO)
        {
            //Ve se o Id realmente pertence a uma categoria
            var category = await EnsureCategoryExists(id);

            //Valida os dados
            var validationResult = await _validatorUpdate.ValidateAsync(categoryDTO);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            //Ve se o nome da categoria já existe
            if (!string.IsNullOrWhiteSpace(categoryDTO.Name) && categoryDTO.Name != category.Name)
            {
                if (await _categoryRepository.GetByNameExactAsync(categoryDTO.Name) is not null)
                    throw new Exception($"Category name '{categoryDTO.Name}' already exists.");
            }

            category.UpdateFromDTO(categoryDTO);

            await _unitOfWork.CommitAsync();
            return _categoryMapper.ToCategoryResponseDTO(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await EnsureCategoryExists(id);

            var products = await _productRepository.GetByCategoryIdAsync(id);
            if (products.Any())
                throw new Exception($"Cannot delete category because it has {products.Count} products.");

            await _categoryRepository.DeleteAsync(category);
            await _unitOfWork.CommitAsync();
        }


        private async Task<Category?> EnsureCategoryExists(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id) ?? throw new Exception("Category doesn't exist.");
        }
    }
}
