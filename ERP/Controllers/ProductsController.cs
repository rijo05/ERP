using ERP.Application.DTOs.Product;
using ERP.Application.DTOs.User;
using ERP.Application.Interfaces;
using ERP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    //GetAll() - Substituido por GetProducts()
    //[HttpGet]
    //public async Task<ActionResult<List<ProductResponseDTO>>> GetAll()
    //{
    //    var products = await _productService.GetAllProductsAsync();

    //    return Ok(products);
    //}

    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDTO>>> GetProducts([FromQuery] ProductFilterDTO productFilterDTO)
    {
        var products = await _productService.GetFilteredProductsAsync(productFilterDTO);

        return Ok(products);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponseDTO>> GetById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
            return NotFound($"Product with ID '{id}' not found.");

        return Ok(product);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<ProductResponseDTO>>> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required.");

        var products = await _productService.GetProductsByNameAsync(name);
        if (products.Count == 0)
            return NotFound($"No products found with name '{name}'.");

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDTO>> Create([FromBody] CreateProductDTO productDTO)
    {
        if (productDTO is null)
            return BadRequest("Product data must be provided.");

        var product = await _productService.AddProductAsync(productDTO);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product
            );
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<ProductResponseDTO>> Update(Guid id, [FromBody] UpdateProductDTO productDTO)
    {
        if (productDTO is null)
            return BadRequest("Product data must be provided.");

        var product = await _productService.UpdateProductAsync(id, productDTO);
        return Ok(product);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }


    [HttpPatch("{id:guid}/inventory")]
    public async Task<ActionResult<ProductResponseDTO>> UpdateInventory(Guid id, [FromBody] UpdateProductInventoryDTO inventoryDTO)
    {
        if (inventoryDTO is null)
            return BadRequest("Product data must be provided.");

        var product = await _productService.UpdateInventory(id, inventoryDTO);

        return Ok(product);
    }
}
