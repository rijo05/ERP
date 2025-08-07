using ERP.Application.DTOs.Order;
using ERP.Application.DTOs.User;
using ERP.Application.Services;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using ERP.Domain.ValueObjects;

namespace ERP.Application.Mappers;

public class OrderMapper
{
    private readonly HateoasLinkService _hateoasLinkService;
    private readonly OrderItemMapper _orderItemMapper;

    public OrderMapper(HateoasLinkService hateoasLinkService, OrderItemMapper orderItemMapper)
    {
        _hateoasLinkService = hateoasLinkService;
        _orderItemMapper = orderItemMapper;
    }

    public OrderResponseDTO ToOrderResponseDTO(Order order)
    {
        return new OrderResponseDTO
        {
            Id = order.Id,
            UserId = order.UserId,
            CustomerId = order.CustomerId,
            OrderStatus = order.OrderStatus,
            CreatedAt = order.CreatedAt,
            LastUpdatedAt = order.LastUpdatedAt,
            SubmittedAt = order.SubmittedAt,
            Items = _orderItemMapper.ToOrderItemResponseDTOList(order.Items.ToList())
        };
    }

    public List<OrderResponseDTO> ToOrderResponseDTOList(List<Order> orders)
    {
        return orders.Select(x => ToOrderResponseDTO(x)).ToList();
    }

    private Dictionary<string, object> GenerateLinks(Order order)
    {
        return _hateoasLinkService.GenerateLinksCRUD(
                    order.Id,
                    "Orders",
                    "GetById",
                    "Update",
                    "Delete"
        );
    }
}
