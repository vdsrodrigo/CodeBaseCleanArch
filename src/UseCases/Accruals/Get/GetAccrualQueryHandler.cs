using Application.Accruals.Create;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AccrualAggregate;
using Domain.AccrualAggregate.Specifications;

namespace Application.Accruals.Get;

/// <summary>
/// As Queries não precisam necessariamente usar métodos de repositório, mas podem se for conveniente.
/// </summary>
public class GetAccrualQueryHandler(IReadRepository<Accrual> repository) : IQueryHandler<GetAccrualQuery, Result<AccrualDTO>>
{
    public async Task<Result<AccrualDTO>> Handle(GetAccrualQuery request, CancellationToken cancellationToken)
    {
        var spec = new AccrualByIdSpec(request.AccrualId);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (entity == null) return Result.NotFound();

        return new AccrualDTO(entity.Id, entity.MemberNumber, entity.Amount, entity.Partner, entity.PhoneNumber?.Number ?? "");
    }
}