using Ardalis.Result;
using Domain.UseCases.Accruals.Get;
using Domain.UseCases.Accruals.Update;
using FastEndpoints;
using MediatR;

namespace WebApi.Accruals;

public class Update(IMediator mediator) : Endpoint<UpdateAccrualRequest, UpdateAccrualResponse>
{ 
    public override void Configure()
    {
        Put(UpdateAccrualRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAccrualRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateAccrualCommand(request.Id, request.Amount), cancellationToken);
        
        if(result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        
        var query = new GetAccrualQuery(request.AccrualId);
        var queryResult = await mediator.Send(query, cancellationToken);

        if (queryResult.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (queryResult.IsSuccess)
        {
            var dto = queryResult.Value;
            Response = new UpdateAccrualResponse(new AccrualRecord(dto.Id, dto.MemberNumber, dto.Amount, dto.Partner, dto.PhoneNumber));
            return;
        }
    }
}