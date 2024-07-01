using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.Accruals.Delete;

public record DeleteAccrualCommand(int AccrualId) : ICommand<Result>;