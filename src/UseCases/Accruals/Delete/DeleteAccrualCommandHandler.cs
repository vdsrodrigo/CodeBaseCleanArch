using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.Interfaces;

namespace Application.Accruals.Delete;

public class DeleteAccrualCommandHandler(IDeleteAccrualService deleteAccrualService)
    : ICommandHandler<DeleteAccrualCommand, Result>
{
    public Task<Result> Handle(DeleteAccrualCommand request, CancellationToken cancellationToken)
    {
        // Esta abordagem mantem eventos de domínio no modelo de domínio (projeto Core), aqui torna-se um repasse
        // Prefira usar o serviço para que o comportamento do evento **domínio** permaneça no modelo **domínio** (projeto Core)
        return deleteAccrualService.DeleteAccrual(request.AccrualId);

        // Outra abordagem: Faça o trabalho aqui, incluindo o envio de eventos de domínio - altere o evento de interno para público
        // var aggregateToDelete = await _repository.GetByIdAsync(request.AccrualId);
        // if (aggregateToDelete == null) return Result.NotFound();

        // await _repository.DeleteAsync(aggregateToDelete);
        // var domainEvent = new AccrualDeletedEvent(request.AccrualId);
        // await _mediator.Publish(domainEvent);
        // return Result.Success();
    }
}