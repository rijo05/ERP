namespace ERP.Domain.Common
{
    public static class Guard
    {
        public static void AgainstNullOrEmpty(string? value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException($"{parameterName} não pode ser nulo ou vazio", parameterName);
        }

        public static void AgainstNegativeOrZero(decimal value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"{parameterName} tem de ser maior que zero", parameterName);
        }
        
        public static void AgainstTooLong(string value, string parameterName, int maxLength)
        {
            if (value != null && value.Length > maxLength)
                throw new ArgumentException($"{parameterName} não pode ter mais de {maxLength} caracteres", parameterName);
        }

        public static void AgainstTooShort(string value, string parameterName, int minLength)
        {
            if (value == null || value.Length < minLength)
                throw new ArgumentException($"{parameterName} tem de ter no mínimo {minLength} caracteres", parameterName);
        }
    }
}
