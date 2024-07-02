using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Domain.UseCases.Accruals.Update;

public record UpdateAccrualCommand(int AccrualId, int NewAmount) : ICommand<Result<AccrualDTO>>;
