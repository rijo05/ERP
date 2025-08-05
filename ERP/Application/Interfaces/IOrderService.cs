using ERP.Domain.Entities;
using ERP.Domain.Enums;

namespace ERP.Application.Interfaces;

public interface IOrderService
{
    public Task<List<Order>> GetAllOrdersAsync();
    public Task<Order> GetOrderByIdAsync(Guid id);
    public Task<List<OrderItem>> GetOrderItemsAsync(Guid id);
    public Task<List<Order>> GetFilteredOrdersAsync(Order order); //OrderFilterDTO no futuro
    public Task<Order> CreateOrderAsync(Order order); //talvez um DTO - criar a order draft
    public Task<Order> SubmitOrderAsync(Guid id);
    public Task<bool> CancelOrderAsync(Guid id);
    public Task<Order> CompleteOrderAsync(Guid orderId);
    public Task<Order> InvoiceOrderAsync(Guid orderId);
    public Task<OrderItem> UpdateQuantityAsync(Guid orderId, Guid itemId ,int quantity, OrderItemType orderItemType);
    public Task<OrderItem> AddItemAsync(Guid orderId, Guid itemId, int quantity, OrderItemType orderItemType);
    public Task RemoveItemAsync(Guid orderId, Guid itemId, OrderItemType orderItemType);
    public Task<Order> UpdateOrderAsync(Guid orderId, Order order); //talvez um dto
}
