using ERP.Application.DTOs.Order;
using ERP.Application.DTOs.OrderItems;
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

    #region GETs

    public Task<List<OrderResponseDTO>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderResponseDTO>> GetFilteredOrdersAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> GetOrderByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderItemResponseDTO>> GetOrderItemsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region CHANGE ORDER STATUS

    public Task<bool> CancelOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> CompleteOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> InvoiceOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> SubmitOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region ADD, CREATE, REMOVE, UPDATES

    public Task<OrderResponseDTO> CreateOrderAsync(CreateOrderDTO orderDTO)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> AddItemAsync(UpdateOrderItemDTO itemDTO)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItemAsync(UpdateOrderItemDTO itemDTO)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> UpdateQuantityAsync(UpdateOrderItemDTO itemDTO)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> UpdateOrderAsync(Guid orderId, Order order)
    {
        throw new NotImplementedException();
    }

    #endregion
}
