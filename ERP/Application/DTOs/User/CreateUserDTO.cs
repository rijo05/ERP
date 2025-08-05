using ERP.Domain.ValueObjects;

namespace ERP.Application.DTOs.User
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
    }
}
