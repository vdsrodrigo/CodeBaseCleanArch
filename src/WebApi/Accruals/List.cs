using Ardalis.Result;
using Domain.UseCases.Accruals;
using Domain.UseCases.Accruals.List;
using FastEndpoints;
using MediatR;

namespace WebApi.Accruals;

public class List(IMediator mediator) : EndpointWithoutRequest<AccrualListResponse>
{
    public override void Configure()
    {
        Get("/Accruals");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        Result<IEnumerable<AccrualDTO>> result = await mediator.Send(new ListAccrualsQuery(null, null), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new AccrualListResponse
            {
                Accruals = result.Value.Select(c => new AccrualRecord(c.Id, c.MemberNumber, c.Amount, c.Partner, c.PhoneNumber)).ToList()
            };
        }
    }
}