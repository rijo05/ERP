using ERP.Application.DTOs.Product;
using ERP.Application.Interfaces;
using ERP.Application.Mappers;
using ERP.Application.Validators.Common;
using ERP.Application.Validators.ProductValidator;
using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using ERP.Domain.ValueObjects;
using ERP.Persistence.Repository;
using FluentValidation;
using Humanizer;

namespace ERP.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateProductDTO> _validatorCreate;
    private readonly IValidator<UpdateProductDTO> _validatorUpdate;
    private readonly IValidator<UpdateProductInventoryDTO> _validatorUpdateInvetory;
    private readonly ProductMapper _productMapper;
    private readonly HateoasLinkService _hateoasLinkService;

    public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ICategoryRepository categoryRepository, IValidator<UpdateProductDTO> validatorUpdate, IValidator<CreateProductDTO> validatorCreator, IValidator<UpdateProductInventoryDTO> validatorUpdateInventory,ProductMapper mapper, HateoasLinkService hateoasLinkService)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _validatorCreate = validatorCreator;
        _validatorUpdate = validatorUpdate;
        _validatorUpdateInvetory = validatorUpdateInventory;
        _productMapper = mapper;
        _hateoasLinkService = hateoasLinkService;
    }

    public async Task<List<ProductResponseDTO>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _productMapper.ToProductResponseDTOList(products);
    }

    public async Task<List<ProductResponseDTO>> GetFilteredProductsAsync(ProductFilterDTO productFilter)    
    {
        if (!string.IsNullOrWhiteSpace(productFilter.CategoryName))
        {
            var category = await _categoryRepository.GetByNameExactAsync(productFilter.CategoryName);
            if (category is null)
                return new List<ProductResponseDTO>();
            productFilter.CategoryId = category.Id;
        }

        var products = await _productRepository.GetFilteredAsync(productFilter);

        return _productMapper.ToProductResponseDTOList(products);
    }

    public async Task<ProductResponseDTO?> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            throw new Exception("Product not found");

        return _productMapper.ToProductResponseDTO(product);
    }

    public async Task<List<ProductResponseDTO>> GetProductsByNameAsync(string name)
    {
        var products = await _productRepository.GetByNameAsync(name);

        if (products.Count == 0)
            throw new Exception($"No products found with name '{name}'.");

        return _productMapper.ToProductResponseDTOList(products);
    }

    public async Task<ProductResponseDTO> AddProductAsync(CreateProductDTO productDTO)
    {
        //Validar os dados
        var validationResult = await _validatorCreate.ValidateAsync(productDTO);
        if (!validationResult.IsValid) 
            throw new ValidationException(validationResult.Errors);


        //Validar se a categoria existe
        await EnsureCategoryExists(Guid.Parse(productDTO.CategoryId));


        var product = new Product(productDTO.Name, productDTO.Price, Guid.Parse(productDTO.CategoryId), productDTO.Stock, productDTO.MinimumStockLevel, productDTO.Description);
        await _productRepository.AddAsync(product);
        await _unitOfWork.CommitAsync();
        return _productMapper.ToProductResponseDTO(product);
    }

    public async Task<ProductResponseDTO> UpdateProductAsync(Guid id, UpdateProductDTO productDTO)
    {
        //Ver se o produto realmente existe
        var product = await EnsureProductExists(id);


        //Validar os dados
        var validationResult = await _validatorUpdate.ValidateAsync(productDTO);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);


        //Ver se a nova categoria vai ser alterada e se sim se existe
        if (!string.IsNullOrEmpty(productDTO.CategoryId))
            await EnsureCategoryExists(Guid.Parse(productDTO.CategoryId));


        product.UpdateFromDTO(productDTO);
        await _unitOfWork.CommitAsync();
        return _productMapper.ToProductResponseDTO(product);
    }      

    public async Task DeleteProductAsync(Guid id)
    {
        //Ver se o produto existe
        var product = await EnsureProductExists(id);

        await _productRepository.DeleteAsync(product);
        await _unitOfWork.CommitAsync();
    }

    public async Task<ProductResponseDTO> UpdateInventory(Guid id, UpdateProductInventoryDTO inventoryDTO)
    {
        //Ver se o produto realmente existe
        var product = await EnsureProductExists(id);

        //Validar os dados
        var validationResult = await _validatorUpdateInvetory.ValidateAsync(inventoryDTO);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        product.UpdateInventoryFromDTO(inventoryDTO);
        await _unitOfWork.CommitAsync();
        return _productMapper.ToProductResponseDTO(product);
    }


    private async Task EnsureCategoryExists(Guid categoryId)
    {
        if (await _categoryRepository.GetByIdAsync(categoryId) is null)
            throw new Exception($"Category with ID {categoryId} does not exist.");
    }
    private async Task<Product?> EnsureProductExists(Guid id)
    {
        return await _productRepository.GetByIdAsync(id) ?? throw new Exception("Product doesn't exist.");
    }
}
