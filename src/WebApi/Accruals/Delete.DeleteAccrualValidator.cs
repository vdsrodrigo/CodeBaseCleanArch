using FastEndpoints;
using FluentValidation;

namespace WebApi.Accruals;

public class DeleteAccrualValidator : Validator<DeleteAccrualRequest>
{
    public DeleteAccrualValidator()
    {
        RuleFor(x => x.AccrualId)
            .GreaterThan(0)
            .WithMessage("AccrualId must be greater than 0.");
    }
}