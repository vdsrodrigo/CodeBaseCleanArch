using FastEndpoints;
using FluentValidation;

namespace WebApi.Accruals;

public class GetAccrualValidator : Validator<GetAccrualByIdRequest>
{
    public GetAccrualValidator()
    {
        RuleFor(x => x.AccrualId)
            .GreaterThan(0)
            .WithMessage("AccrualId must be greater than 0.");
    }
}