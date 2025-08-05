using ERP.Application.DTOs.Product;
using ERP.Application.DTOs.User;
using ERP.Domain.Entities;
using ERP.Domain.ValueObjects;

namespace ERP.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByEmailAsync(Email email);
    public Task<List<User>> GetByNameAsync(string username);
    public Task<List<User>> GetFilteredAsync(UserFilterDTO userFilter);
}
