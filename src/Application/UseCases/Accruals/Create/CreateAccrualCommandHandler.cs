using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AccrualAggregate;

namespace Domain.UseCases.Accruals.Create;

public class CreateAccrualCommandHandler(IRepository<Accrual> repository)
    : ICommandHandler<CreateAccrualCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateAccrualCommand request, CancellationToken cancellationToken)
    {
        var newAccrual = new Accrual(request.MemberNumber, request.Amount, request.AccrualDate, request.Partner);
        if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            newAccrual.SetPhoneNumber(request.PhoneNumber);
        }

        var createdAccrual = await repository.AddAsync(newAccrual, cancellationToken);
        return createdAccrual.Id;
    }
}