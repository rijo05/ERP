using ERP.Application.DTOs.Product;
using ERP.Application.Validators.Common;
using FluentValidation;

namespace ERP.Application.Validators.ProductValidator;

public class UpdateProductInventoryDTOValidator : AbstractValidator<UpdateProductInventoryDTO>
{
    public UpdateProductInventoryDTOValidator()
    {
        RuleFor(x => x.Stock).StockRules()
            .When(x => x is not null);

        RuleFor(x => x.MinimumStockLevel).MinimumStockLevelRules()
            .When(x => x is not null);
    }
}
