using ERP.Domain.ValueObjects;
using ERP.Domain.Common;

namespace ERP.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Role Role { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        private User() { }

        public User(string name, Email email, Role role, string passwordHash)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(passwordHash, nameof(passwordHash));


            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Role = role;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void ChangeEmail(string newEmail)
        {
            Email = new Email(newEmail);
        }
        public void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
