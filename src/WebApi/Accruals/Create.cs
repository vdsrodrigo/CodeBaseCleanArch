using Application.Accruals.Create;
using Domain.AccrualAggregate;
using Domain.Shared;
using FastEndpoints;
using MediatR;

namespace WebApi.Accruals;

public class Create(IMediator mediator) : Endpoint<CreateAccrualRequest, CreateAccrualResponse>
{
    public override void Configure()
    {
        Post(CreateAccrualRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            // Os documentos XML são usados por padrão, podendo ter campos customizados:
            s.Summary = "Cria um acúmulo.";
            s.Description = "Cria um acúmulo quando todos os campos forem válidos.";
            s.ExampleRequest = new CreateAccrualRequest
            {
                MemberNumber = "32425399978",
                Partner = Partner.GrupoPao,
                Amount = 100,
                AccrualDate = DateTime.UtcNow,
                PhoneNumber = "5511928645095"
            };
        });
    }

    public override async Task HandleAsync(CreateAccrualRequest request, CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new CreateAccrualCommand(request.MemberNumber, request.Amount, request.AccrualDate, request.Partner, request.PhoneNumber),
                cancellationToken);

        if (result.IsSuccess)
        {
            Response = new CreateAccrualResponse(result.Value, request.MemberNumber);
            return;
        }

        // TODO: Incluir casos de falha e outras regras.
    }
}