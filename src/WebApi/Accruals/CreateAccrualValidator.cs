using FastEndpoints;
using FluentValidation;
using Infrastructure.Config;

namespace WebApi.Accruals;

public class CreateAccrualValidator : Validator<CreateAccrualRequest>
{
    public CreateAccrualValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required.")
            .MinimumLength(11)
            .MaximumLength(13);
    }
}