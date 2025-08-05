using ERP.Application.DTOs.Product;
using ERP.Application.DTOs.User;
using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using ERP.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace ERP.Persistence.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
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

        public async Task<List<User>> GetFilteredAsync(UserFilterDTO dto)
        {
            var query = _appDbContext.Users.AsQueryable();

            //NAME
            if (!string.IsNullOrWhiteSpace(dto.Name))
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{dto.Name}%"));

            //ISACTIVE
            if (dto.IsActive.HasValue)
                query = query.Where(p => p.IsActive == dto.IsActive.Value);

            //EMAIL
            if (!string.IsNullOrWhiteSpace(dto.Email))
                query = query.Where(p => p.Email != null && EF.Functions.Like(p.Email.Value, $"%{dto.Email}%"));

            //ROLE
            if (!string.IsNullOrWhiteSpace(dto.Role))
                query = query.Where(p => p.Role != null && EF.Functions.Like(p.Role.roleName.ToString(), $"%{dto.Role}%"));


            query = dto.Sort switch
            {
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Name)
            };

            var page = Math.Max(dto.Page, 1);
            var pageSize = Math.Clamp(dto.PageSize, 1, 100);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
