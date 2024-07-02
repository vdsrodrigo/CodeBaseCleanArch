using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.Accruals.Update;

public record UpdateAccrualCommand(int AccrualId, int NewAmount) : ICommand<Result<AccrualDTO>>;
