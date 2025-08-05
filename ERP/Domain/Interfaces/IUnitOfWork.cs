namespace ERP.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
