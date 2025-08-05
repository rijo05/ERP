using ERP.Domain.Entities;
using ERP.Domain.Enums;

namespace ERP.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    public Task<List<OrderItem>> GetOrderItemsAsync(Guid id);
    public Task<List<Order>> GetFilteredOrdersAsync(Order order); //OrderFilterDTO no futuro
}
