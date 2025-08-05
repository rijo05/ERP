using MediatR;

namespace ERP.Application.Events.Products.Handler;

public class ProductOutOfStockHandler : INotificationHandler<ProductOutOfStockEvent>
{
    public Task Handle(ProductOutOfStockEvent ev, CancellationToken token)
    {
        //Todo - Enviar email
        return Task.CompletedTask;
    }
}
