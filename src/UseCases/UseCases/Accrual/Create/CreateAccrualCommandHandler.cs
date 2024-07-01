using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.UseCases.Accrual.Create;

public class CreateAccrualCommandHandler(IRepository<Domain.AccrualAggregate.Accrual> repository) : ICommandHandler<CreateAccrualCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateAccrualCommand request, CancellationToken cancellationToken)
    {
        var newAccrual = new Domain.AccrualAggregate.Accrual(request.MemberNumber, request.Amount, request.AccrualDate, request.Partner);
        var createdAccrual = await repository.AddAsync(newAccrual, cancellationToken);
        return createdAccrual.Id;
    }
}