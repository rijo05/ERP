using ERP.Application.DTOs.User;
using ERP.Application.Validators.Common;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using FluentValidation;

namespace ERP.Application.Validators.UserValidator
{
    internal sealed class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator() 
        {
            RuleFor(x => x.Name).UserNameRules();

            RuleFor(x => x.Email).EmailRules();

            RuleFor(x => x.Password).PasswordRules();

            RuleFor(x => x.Role).RoleRules();
        }
    }
}
