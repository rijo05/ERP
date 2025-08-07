using ERP.Application.DTOs.OrderItems;
using ERP.Domain.Entities;
using ERP.Domain.Enums;

namespace ERP.Application.DTOs.Order;

public class OrderResponseDTO
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid? CustomerId { get; init; }
    public OrderStatus OrderStatus { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdatedAt { get; init; }
    public DateTime? SubmittedAt { get; init; }

    private readonly List<OrderItemResponseDTO> _items = new();
    public IReadOnlyCollection<OrderItemResponseDTO> Items => _items.AsReadOnly();

    public decimal TotalPrice => _items.Sum(x => x.Total);
}
