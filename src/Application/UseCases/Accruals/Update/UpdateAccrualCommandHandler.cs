using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.AccrualAggregate;

namespace Domain.UseCases.Accruals.Update;

public class UpdateAccrualCommandHandler(IRepository<Accrual> repository) : ICommandHandler<UpdateAccrualCommand, Result<AccrualDTO>>
{
    public async Task<Result<AccrualDTO>> Handle(UpdateAccrualCommand request, CancellationToken cancellationToken)
    {
        var existingAccrual = await repository.GetByIdAsync(request.AccrualId, cancellationToken);
        if (existingAccrual == null)
        {
            return Result.NotFound();
        }

        existingAccrual.UpdateAmount(request.NewAmount);

        await repository.UpdateAsync(existingAccrual, cancellationToken);

        return Result.Success(new AccrualDTO(existingAccrual.Id,
            existingAccrual.MemberNumber,existingAccrual.Amount, existingAccrual.Partner, existingAccrual.PhoneNumber?.Number ?? ""));
    }
}