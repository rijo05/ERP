using ERP.Application.DTOs.Order;
using ERP.Application.DTOs.OrderItems;
using ERP.Application.Interfaces;
using ERP.Application.Mappers;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using ERP.Domain.Interfaces;
using ERP.Persistence.Repository;

namespace ERP.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderMapper _orderMapper;
    private readonly OrderItemMapper _orderItemMapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository, OrderMapper orderMapper, OrderItemMapper orderItemMapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderMapper = orderMapper;
        _orderItemMapper = orderItemMapper;
        _unitOfWork = unitOfWork;
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

    public async Task<OrderResponseDTO> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
            throw new Exception("Order not found");

        return _orderMapper.ToOrderResponseDTO(order);
    }

    public async Task<List<OrderItemResponseDTO>> GetOrderItemsAsync(Guid id)
    {
        //verificar que o pedido existe
        if (!await _orderRepository.CheckIfExistsAsync(id))
            throw new Exception("Order doesnt exist");
            
        var orderItems = await _orderRepository.GetOrderItemsAsync(id);

        return _orderItemMapper.ToOrderItemResponseDTOList(orderItems);
    }

    #endregion

    #region CHANGE ORDER STATUS

    public async Task CancelOrderAsync(Guid id)
    {
        var order = await EnsureOrderExists(id);

        order.Cancel(); 
        await _unitOfWork.CommitAsync();
    }

    public Task<OrderResponseDTO> CompleteOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDTO> InvoiceOrderAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderResponseDTO> SubmitOrderAsync(Guid id)
    {
        var order = await EnsureOrderExists(id);

        order.Submit();
        await _unitOfWork.CommitAsync();
        return _orderMapper.ToOrderResponseDTO(order);
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


    #region private
    private async Task<Order?> EnsureOrderExists(Guid id)
    {
        return await _orderRepository.GetByIdAsync(id) ?? throw new Exception("Order doesn't exist.");
    }
    #endregion
}
