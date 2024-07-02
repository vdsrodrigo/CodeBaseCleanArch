using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AccrualAggregate;
using Domain.AccrualAggregate.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Services;

public class DeleteAccrualService(IRepository<Accrual> repository, IMediator mediator, ILogger<DeleteAccrualService> logger) : IDeleteAccrualService
{
    public async Task<Result> DeleteAccrual(int accrualId)
    {
        logger.LogInformation("Deleting Accrual {accrualId}", accrualId);
        Accrual? accrualToDelete = await repository.GetByIdAsync(accrualId);
        if (accrualToDelete == null) return Result.NotFound();

        await repository.DeleteAsync(accrualToDelete);
        var domainEvent = new AccrualDeletedEvent(accrualId);
        await mediator.Publish(domainEvent);
        return Result.Success();
    }
}