using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Domain.UseCases.Accruals.Delete;

public record DeleteAccrualCommand(int AccrualId) : ICommand<Result>;