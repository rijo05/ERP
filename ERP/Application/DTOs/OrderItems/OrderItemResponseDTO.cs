using ERP.Domain.Enums;

namespace ERP.Application.DTOs.OrderItems;

public class OrderItemResponseDTO
{
    public Guid OrderId { get; init; }
    public Guid ReferenceId { get; init; }
    public string Name { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public OrderItemType ItemType { get; init; }
    public decimal Total => Quantity * UnitPrice;
}
