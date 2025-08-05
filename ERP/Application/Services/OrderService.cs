using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using ERP.Domain.Interfaces;

namespace ERP.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public Task<OrderItem> AddItemAsync(Guid orderId, Guid itemId, int quantity, OrderItemType orderItemType)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancelOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> CompleteOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> CreateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetFilteredOrdersAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderItem>> GetOrderItemsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> InvoiceOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItemAsync(Guid orderId, Guid itemId, OrderItemType orderItemType)
    {
        throw new NotImplementedException();
    }

    public Task<Order> SubmitOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateOrderAsync(Guid orderId, Order order)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem> UpdateQuantityAsync(Guid orderId, Guid itemId, int quantity, OrderItemType orderItemType)
    {
        throw new NotImplementedException();
    }
}
