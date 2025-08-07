using ERP.Application.DTOs.Order;
using ERP.Application.DTOs.OrderItems;
using ERP.Application.Services;
using ERP.Domain.Entities;

namespace ERP.Application.Mappers;

public class OrderItemMapper
{
    private readonly HateoasLinkService _hateoasLinkService;

    public OrderItemMapper(HateoasLinkService hateoasLinkService)
    {
        _hateoasLinkService = hateoasLinkService;
    }

    public OrderItemResponseDTO ToOrderItemResponseDTO(OrderItem item)
    {
        return new OrderItemResponseDTO
        {
            OrderId = item.OrderId,
            ReferenceId = item.ReferenceId,
            Name = item.Name,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity,
            ItemType = item.ItemType
        };
    }

    public List<OrderItemResponseDTO> ToOrderItemResponseDTOList(List<OrderItem> items)
    {
        return items.Select(x => ToOrderItemResponseDTO(x)).ToList();
    }

    //mudar
    private Dictionary<string, object> GenerateLinks(OrderItem item)
    {
        return _hateoasLinkService.GenerateLinksCRUD(
                    item.ReferenceId,
                    "OrderItems",
                    "GetById",
                    "Update",
                    "Delete"
        );
    }
}
