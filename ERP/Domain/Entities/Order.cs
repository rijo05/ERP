using ERP.Domain.Enums;

namespace ERP.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? CustomerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }
    public DateTime? SubmittedAt { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => _items.Sum(x => x.Total);

    private Order() { }

    public Order(Guid userId, Guid? customerId)
    {
        UserId = userId;
        CustomerId = customerId;
        OrderStatus = OrderStatus.Draft;
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public void AddItem(OrderItem item)
    {
        if(item is null) throw new ArgumentNullException("item");

        if (OrderStatus != OrderStatus.Draft)
            throw new ArgumentException("Cant add products right now");


        var existing = _items.FirstOrDefault(x => x.ReferenceId == item.ReferenceId && x.ItemType == item.ItemType);

        if (existing is not null)
            existing.UpdateQuantity(item.Quantity);
        else
            _items.Add(item);
    }

    public void RemoveItem(Guid itemId, OrderItemType itemType)
    {
        if (OrderStatus != OrderStatus.Draft)
            throw new ArgumentException("Cant add products right now");

        var existing = _items.FirstOrDefault(x => x.ReferenceId == itemId && x.ItemType == itemType);
        if (existing is not null)
        {
            _items.Remove(existing);
            LastUpdatedAt = DateTime.UtcNow;
        }     
    }
    public void Cancel()
    {
        //quando dou submit a order, mexo logo no stock??
        //quando ]e q uma order fica completed?
        //se so mexer no stock no complete como resolver conflitos de stock no submitted
        //TODO() - TOSEE()
        if (OrderStatus == OrderStatus.Completed || OrderStatus == OrderStatus.Invoiced || OrderStatus == OrderStatus.Cancelled)
            throw new Exception($"Cant cancel a {this.OrderStatus} order");

        OrderStatus = OrderStatus.Cancelled;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public void Submit()
    {
        if (!_items.Any())
            throw new Exception("Cant submit an order without items");

        if (OrderStatus != OrderStatus.Draft)
            throw new Exception("Cant submit an order that isnt a draft");

        OrderStatus = OrderStatus.Submitted;
        LastUpdatedAt = DateTime.UtcNow;
        SubmittedAt = DateTime.UtcNow;
    }
}