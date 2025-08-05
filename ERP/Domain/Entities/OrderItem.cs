using ERP.Domain.Enums;
using ERP.Domain.Guard;

namespace ERP.Domain.Entities;

public class OrderItem
{
    public Guid ReferenceId { get; private set; }
    public string Name { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public OrderItemType ItemType { get; private set; }
    public decimal Total => Quantity * UnitPrice;

    private OrderItem() { }

    public static OrderItem FromProduct(Product product, int quantity)
    {
        var orderItem = new OrderItem
        {
            ReferenceId = product.Id,
            Name = product.Name,
            UnitPrice = product.Price,
            Quantity = quantity,
            ItemType = OrderItemType.Product 
        };

        ValidateBuilders(orderItem);
        return orderItem;
    }

    //Passar o service completo ou apenas ID?
    //Service tem campo duration q n e usado aqui, importa?
    //TODO() - TOSEE()
    public static OrderItem FromService(Service service, int quantity)
    {
        var orderItem = new OrderItem
        {
            ReferenceId = service.Id,
            Name = service.Name,
            UnitPrice = service.Price,
            Quantity = quantity,
            ItemType = OrderItemType.Service
        };

        ValidateBuilders(orderItem);
        return orderItem;
    }

    public void UpdateQuantity(int quantityToAdd)
    {
        if (quantityToAdd <= 0)
            throw new Exception("Invalid quantity");

        Quantity += quantityToAdd;
    }

    private static void ValidateBuilders(OrderItem item)
    {
        GuardCommon.AgainstNegativeOrZero(item.Quantity, nameof(item.Quantity));
    }
}


