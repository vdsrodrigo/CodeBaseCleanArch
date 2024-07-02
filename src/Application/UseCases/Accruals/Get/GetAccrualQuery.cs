using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Domain.UseCases.Accruals.Get;

public record GetAccrualQuery(int AccrualId) : IQuery<Result<AccrualDTO>>;