using ERP.Application.DTOs.Order;
using ERP.Application.DTOs.OrderItems;
using ERP.Domain.Entities;
using ERP.Domain.Enums;

namespace ERP.Application.Interfaces;

public interface IOrderService
{
    public Task<List<OrderResponseDTO>> GetAllOrdersAsync(); //trocar pelo filtered PROVAVELMENTE
    public Task<OrderResponseDTO> GetOrderByIdAsync(Guid id);
    public Task<List<OrderItemResponseDTO>> GetOrderItemsAsync(Guid id);
    public Task<List<OrderResponseDTO>> GetFilteredOrdersAsync(Order order); //OrderFilterDTO no futuro
    public Task<OrderResponseDTO> CreateOrderAsync(CreateOrderDTO orderDTO);
    public Task<OrderResponseDTO> SubmitOrderAsync(Guid id);
    public Task<bool> CancelOrderAsync(Guid id);
    public Task<OrderResponseDTO> CompleteOrderAsync(Guid orderId);
    public Task<OrderResponseDTO> InvoiceOrderAsync(Guid orderId);
    public Task<OrderResponseDTO> UpdateQuantityAsync(UpdateOrderItemDTO itemDTO);
    public Task<OrderResponseDTO> AddItemAsync(UpdateOrderItemDTO itemDTO);
    public Task RemoveItemAsync(UpdateOrderItemDTO itemDTO);
    public Task<OrderResponseDTO> UpdateOrderAsync(Guid orderId, Order order); //talvez um updateDTO
}
