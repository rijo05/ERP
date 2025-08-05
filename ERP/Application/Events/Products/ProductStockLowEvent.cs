using ERP.Domain.Common;

namespace ERP.Application.Events.Products;

public class ProductStockLowEvent : IDomainEvent
{
    public Guid ProductId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public ProductStockLowEvent(Guid productId)
    {
        ProductId = productId;
    }
}
