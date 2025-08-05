using ERP.Application.DTOs.Product;
using ERP.Application.Validators.Common;
using FluentValidation;

namespace ERP.Application.Validators.ProductValidator
{
    internal sealed class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator() 
        {
            RuleFor(x => x.Name).NameRules();

            RuleFor(x => x.Description).DescriptionRules();

            RuleFor(x => x.Price).PriceRules();

            RuleFor(x => x.CategoryId).IsValidGuid();

            RuleFor(x => x.Stock).StockRules();

            RuleFor(x => x.MinimumStockLevel).MinimumStockLevelRules();
        }
    }
}
