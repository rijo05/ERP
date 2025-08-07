using ERP.Application.DTOs.User;
using ERP.Application.Interfaces;
using ERP.Application.Services;
using ERP.Domain.Entities;
using ERP.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ERP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    //GetAll() - Substituido por GetUsers()
    //[HttpGet]
    //public async Task<ActionResult<List<ProductResponseDTO>>> GetAll()
    //{
    //    var products = await _productService.GetAllProductsAsync();

    //    return Ok(products);
    //}

    [HttpGet]
    public async Task<ActionResult<List<UserResponseDTO>>> GetUsers([FromQuery] UserFilterDTO userFilter)
    {
        var users = await _userService.GetFilteredUsersAsync(userFilter);

        return Ok(users);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponseDTO>> GetById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserResponseDTO>> GetByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(new Email(email));

        return Ok(user);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<UserResponseDTO>>> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required.");

        var users = await _userService.GetUsersByNameAsync(name);

        return Ok(users);
    }


    [HttpPost]
    public async Task<ActionResult<UserResponseDTO>> Create([FromBody] CreateUserDTO userDTO)
    {
        if (userDTO is null)
            return BadRequest("User data must be provided.");

        var user = await _userService.AddUserAsync(userDTO);

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            user
            );
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<UserResponseDTO>> Update(Guid id, [FromBody] UpdateUserDTO userDTO)
    {
        var user = await _userService.UpdateUserAsync(id, userDTO);
        return Ok(user);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
