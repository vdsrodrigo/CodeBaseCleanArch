using Ardalis.SharedKernel;

namespace Domain.AccrualAggregate.Events;

public sealed class AccrualDeletedEvent(int accrualId) : DomainEventBase
{
    public int AccrualId { get; init; } = accrualId;
}