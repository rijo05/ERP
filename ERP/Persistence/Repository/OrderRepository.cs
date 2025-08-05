using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERP.Persistence.Repository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public Task<List<Order>> GetFilteredOrdersAsync(Order order)
    {
        throw new NotImplementedException();
        //TODO() - CRIAR FILTROS - PRECO, ITENS...
    }

    public async Task<List<OrderItem>> GetOrderItemsAsync(Guid id)
    {
        return await _appDbContext.Set<OrderItem>().Where(x => x.OrderId == id).ToListAsync();
    }
}
