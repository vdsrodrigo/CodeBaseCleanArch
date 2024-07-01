using Application.Accruals.Delete;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace WebApi.Accruals;

public class Delete(IMediator mediator) : Endpoint<DeleteAccrualRequest>
{
    public override void Configure()
    {
        Delete(DeleteAccrualRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteAccrualRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAccrualCommand(request.AccrualId);

        var result = await mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            await SendNoContentAsync(cancellationToken);
            return;
        }
    }
}