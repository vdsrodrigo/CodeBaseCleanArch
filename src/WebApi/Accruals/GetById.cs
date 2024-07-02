using Ardalis.Result;
using Domain.UseCases.Accruals.Get;
using FastEndpoints;
using MediatR;

namespace WebApi.Accruals;

public class GetById(IMediator mediator) : Endpoint<GetAccrualByIdRequest, AccrualRecord>
{
    public override void Configure()
    {
        Get(GetAccrualByIdRequest.Route);
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(GetAccrualByIdRequest request, CancellationToken cancellationToken)
    {
        var query = new GetAccrualQuery(request.AccrualId);
        
        var result = await mediator.Send(query, cancellationToken);
        
        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        
        if (result.IsSuccess)
        {
            Response = new AccrualRecord(result.Value.Id, result.Value.MemberNumber, result.Value.Amount, result.Value.Partner, result.Value.PhoneNumber);
        }
    }
}