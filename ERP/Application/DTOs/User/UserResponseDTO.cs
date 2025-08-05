using ERP.Domain.ValueObjects;
using ERP.Domain.Entities;

namespace ERP.Application.DTOs.User
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEditOwnProfile { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, object> Links { get; set; }
    }
}
