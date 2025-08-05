using ERP.Domain.Enums;

namespace ERP.Domain.Guard
{
    public static class UserGuard
    {
        public static void AgainstNullOrEmptyName(string name)
        {
            GuardCommon.AgainstNullOrEmpty(name, nameof(name));
            GuardCommon.AgainstMaxLength(name, 50, nameof(name));
            GuardCommon.AgainstInvalidFormat(name, @"^[a-zA-Z\s]+$", nameof(name));
        }

        public static void AgainstInvalidEmail(string email)
        {
            GuardCommon.AgainstNullOrEmpty(email, nameof(email));

            const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            GuardCommon.AgainstInvalidFormat(email, emailPattern, nameof(email));
        }

        public static void AgainstInvalidRole(string role)
        {
            GuardCommon.AgainstNullOrEmpty(role, nameof(role));

            if (!Enum.TryParse<RoleType>(role, true, out _))
                throw new ArgumentException($"Invalid role value: {role}", nameof(role));
        }

        public static void AgainstInvalidPassword(string password)
        {
            GuardCommon.AgainstNullOrEmpty(password, nameof(password));
            GuardCommon.AgainstMaxLength(password, 100, nameof(password));

            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$";
            GuardCommon.AgainstInvalidFormat(password, pattern, nameof(password));
        }
    }

}
