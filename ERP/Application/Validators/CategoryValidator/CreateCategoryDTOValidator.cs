using ERP.Application.DTOs.Category;
using ERP.Application.Validators.Common;
using FluentValidation;

namespace ERP.Application.Validators.CategoryValidator
{
    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidator()
        {
            RuleFor(x => x.Name).NameRules();

            RuleFor(x => x.Description).DescriptionRules();
        }
    }
}
