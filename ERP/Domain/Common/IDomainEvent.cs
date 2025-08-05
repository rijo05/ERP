using MediatR;

namespace ERP.Domain.Common;
public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
