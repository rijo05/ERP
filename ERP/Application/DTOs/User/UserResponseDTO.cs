using ERP.Domain.ValueObjects;
using ERP.Domain.Entities;

namespace ERP.Application.DTOs.User;

public class UserResponseDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
    public bool CanDelete { get; init; }
    public bool CanEditOwnProfile { get; init; }
    public bool IsActive { get; init; }
    public Dictionary<string, object> Links { get; init; }
}
