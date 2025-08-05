using ERP.Application.DTOs.Category;
using ERP.Application.Validators.Common;
using FluentValidation;

namespace ERP.Application.Validators.CategoryValidator
{
    public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDTOValidator() 
        {
            RuleFor(x => x.Name).NameRules()
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description).DescriptionRules()
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
