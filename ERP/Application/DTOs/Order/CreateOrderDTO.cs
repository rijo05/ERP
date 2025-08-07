namespace ERP.Application.DTOs.Order;

public class CreateOrderDTO
{
    public Guid UserId { get; set; }
    public Guid? CustomerId { get; set; }
}
