using ERP.Application.DTOs.Product;
using ERP.Application.Validators.Common;
using FluentValidation;

namespace ERP.Application.Validators.ProductValidator
{
    public class UpdateProductDTOValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidator() 
        {
            RuleFor(x => x.Name).NameRules()
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description).DescriptionRules()
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Price).PriceRules()
                .When(x => x.Price.HasValue);

            RuleFor(x => x.CategoryId).IsValidGuid()
                .When(x => !string.IsNullOrEmpty(x.CategoryId));
        }
    }
}
