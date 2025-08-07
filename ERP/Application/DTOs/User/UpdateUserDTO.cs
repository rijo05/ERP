namespace ERP.Application.DTOs.User;

public class UpdateUserDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public bool? IsActive { get; set; }
}
