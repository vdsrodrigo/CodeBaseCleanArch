using Domain.AccrualAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.AccrualAggregate.Handlers;

/// <summary>
/// NOTE: Internal porque AccrualDeletedEvent também está marcado como internal.
/// </summary>
internal class AccrualDeletedHandler(ILogger<AccrualDeletedHandler> logger) : INotificationHandler<AccrualDeletedEvent>
{

    public async Task Handle(AccrualDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Accrual Deleted event for {accrualId}", domainEvent.AccrualId);
        return;
    }
}