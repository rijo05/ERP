using ERP.Application.DTOs.User;
using ERP.Application.Validators.Common;
using ERP.Domain.Enums;
using FluentValidation;

namespace ERP.Application.Validators.UserValidator
{
    internal sealed class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(x => x.Name).UserNameRules()
                                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Email).EmailRules()
                                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password).PasswordRules()
                                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.Role).RoleRules()
                                .When(x => !string.IsNullOrEmpty(x.Role));
        }
    }
}
