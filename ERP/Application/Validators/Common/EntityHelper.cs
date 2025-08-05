using ERP.Domain.Interfaces;
using FluentValidation;

namespace ERP.Application.Validators.Common
{
    public static class EntityHelper
    {
        public static async Task EnsureEntityExistsAsync<TEntity>(IRepository<TEntity> repository, Guid id, string entityName) where TEntity : class
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity is null)
                throw new Exception($"{entityName} with ID {id} does not exist.");
        }
    }
}
