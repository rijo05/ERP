using MediatR;

namespace ERP.Application.Events.Products.Handler;

public class ProductStockLowHandler : INotificationHandler<ProductStockLowEvent>
{
    public Task Handle(ProductStockLowEvent ev, CancellationToken token)
    {
        //Todo - Enviar email
        return Task.CompletedTask;
    }
}
