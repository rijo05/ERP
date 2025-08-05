using ERP.Domain.Enums;
using ERP.Domain.ValueObjects;
using FluentValidation;

namespace ERP.Application.Validators.Common
{
    public static class ValidationRules
    {
        //User name - No numbers allowed
        public static IRuleBuilderOptions<T, string?> UserNameRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name cannot exceed 50 characters")
                        .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces.");
        }

        //Product/category/... name - Numbers allowed
        public static IRuleBuilderOptions<T, string?> NameRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");
        }

        //Optional big text - max 255 characters
        public static IRuleBuilderOptions<T, string?> DescriptionRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                        .MaximumLength(255).WithMessage("Name cannot exceed 255 characters");
        }

        //Email
        public static IRuleBuilderOptions<T, string?> EmailRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                        .NotEmpty().WithMessage("Email is required.")
                        .EmailAddress().WithMessage("Invalid email format");
        }

        //Password
        public static IRuleBuilderOptions<T, string?> PasswordRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                        .NotEmpty().WithMessage("Password is required")
                        .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                        .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$")
                        .WithMessage("Password must have at least one uppercase letter, one number and one special character.");
        }

        //Roles - check if it exists
        public static IRuleBuilderOptions<T, string?> RoleRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                        .NotEmpty().WithMessage("Role is required.")
                        .Must(role => Enum.TryParse<RoleType>(role, true, out _))
                        .WithMessage("Invalid role.");
        }

        //Price - Greater then 0
        public static IRuleBuilderOptions<T, decimal?> PriceRules<T>(this IRuleBuilder<T, decimal?> ruleBuilder)
        {
            return ruleBuilder
                        .NotNull()
                        .GreaterThan(0).WithMessage("Price must be greater than zero");
        }

        // Opcional
        public static IRuleBuilderOptions<T, decimal> PriceRules<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder
                        .GreaterThan(0).WithMessage("Price must be greater than zero");
        }

        // GUID válido
        public static IRuleBuilderOptions<T, string?> IsValidGuid<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("CategoryId is required.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("CategoryId must be a valid GUID.");
        }

        public static IRuleBuilderOptions<T, int?> StockRules<T>(this IRuleBuilder<T, int?> ruleBuilder)
        {
            return ruleBuilder
                        .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater or equal to zero");
        }

        public static IRuleBuilderOptions<T, int?> MinimumStockLevelRules<T>(this IRuleBuilder<T, int?> ruleBuilder)
        {
            return ruleBuilder  
                        .GreaterThanOrEqualTo(0).WithMessage("Minimum stock level must be greater or equal to zero");
        }
    }
}
