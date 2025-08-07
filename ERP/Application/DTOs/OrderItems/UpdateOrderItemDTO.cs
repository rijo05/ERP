using ERP.Domain.Enums;

namespace ERP.Application.DTOs.OrderItems;

public class UpdateOrderItemDTO
{
    public Guid OrderId { get; set; }
    public Guid OrderItemId { get; set; }
    public int? Quantity { get; set; }
    OrderItemType OrderItemType { get; set; }
}
