using ERP.Application.DTOs.User;
using ERP.Domain.Entities;
using ERP.Domain.ValueObjects;

namespace ERP.Application.Interfaces;

public interface IUserService
{
    public Task<List<UserResponseDTO>> GetAllUsersAsync();
    public Task<List<UserResponseDTO>> GetFilteredUsersAsync(UserFilterDTO userFilter);
    public Task<UserResponseDTO?> GetUserByIdAsync(Guid id);
    public Task<List<UserResponseDTO>> GetUsersByNameAsync(string name);
    public Task<UserResponseDTO?> GetUserByEmailAsync(Email email);


    public Task<UserResponseDTO> AddUserAsync(CreateUserDTO userDTO);
    public Task DeleteUserAsync(Guid id);
    public Task<UserResponseDTO> UpdateUserAsync(Guid id, UpdateUserDTO updatedUserDTO);
}
