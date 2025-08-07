using ERP.Application.DTOs.Category;
using ERP.Application.DTOs.Product;
using ERP.Application.Interfaces;
using ERP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        return Ok(categories);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDTO>> GetById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        return Ok(category);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<CategoryResponseDTO>>> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required.");

        var categories = await _categoryService.GetCategoriesByNameAsync(name);

        return Ok(categories);
    }


    [HttpPost]
    public async Task<ActionResult<CategoryResponseDTO>> Create([FromBody] CreateCategoryDTO categoryDTO)
    {
        if (categoryDTO is null)
            return BadRequest("Category data must be provided.");

        var category = await _categoryService.AddCategoryAsync(categoryDTO);

        return CreatedAtAction(
            nameof(GetById),
            new { id = category.Id },
            category
            );
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDTO>> Update(Guid id, [FromBody] UpdateCategoryDTO categoryDTO)
    {
        if (categoryDTO is null)
            return BadRequest("Category data must be provided.");

        var category = await _categoryService.UpdateCategoryAsync(id, categoryDTO);
        return Ok(category);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}
