using ERP.Application.DTOs.User;
using ERP.Application.Services;
using ERP.Domain.Enums;
using ERP.Domain.Guard;
using ERP.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace ERP.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Role Role { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        private User() { }

        public User(string name, Email email, Role role, string password)
        {
            UserGuard.AgainstNullOrEmptyName(name);
            UserGuard.AgainstInvalidEmail(email.Value);
            UserGuard.AgainstInvalidRole(role.ToString());
            UserGuard.AgainstInvalidPassword(password);

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Role = role;
            Password = HashPassword(password);
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void UpdateFromDTO(UpdateUserDTO dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Password))
                UpdatePassword(dto.Password);

            if (!string.IsNullOrWhiteSpace(dto.Name))
                UpdateName(dto.Name);

            if (!string.IsNullOrWhiteSpace(dto.Email))
                UpdateEmail(dto.Email);

            if (!string.IsNullOrWhiteSpace(dto.Role))
                UpdateRole(new Role(dto.Role));

            if (dto.IsActive.HasValue)
                SetActive(dto.IsActive.Value);
        }

        public void UpdatePassword(string? newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentNullException(nameof(newPassword));

            if (BCrypt.Net.BCrypt.Verify(newPassword, this.Password))
                throw new Exception("New password can't be equal to previous password.");

            UserGuard.AgainstInvalidPassword(newPassword);
            Password = HashPassword(newPassword);
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void UpdateName(string? newName)
        {
            UserGuard.AgainstNullOrEmptyName(newName);
            Name = newName;
        }

        public void UpdateEmail(string? newEmail)
        {
            UserGuard.AgainstInvalidEmail(newEmail);
            Email = new Email(newEmail);
        }

        public void UpdateRole(Role? newRole)
        {
            UserGuard.AgainstInvalidRole(newRole.ToString());
            Role = newRole;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
