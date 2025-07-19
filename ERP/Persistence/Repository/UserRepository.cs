using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using ERP.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace ERP.Persistence.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }
                
        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await _appDbContext
                            .Users
                            .FirstOrDefaultAsync(u => u.Email.Value == email.Value);
        }

        public async Task<List<User>> GetByNameAsync(string name)
        {
            return await _appDbContext.Users
                .Where(u => EF.Functions.Like(u.Name, $"%{name}%"))
                .ToListAsync();
        }
    }
}
