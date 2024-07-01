using Ardalis.SharedKernel;

namespace Domain.AccrualAggregate.Events;

internal sealed class AccrualDeletedEvent(int accrualId) : DomainEventBase
{
    public int AccrualId { get; init; } = accrualId;
}