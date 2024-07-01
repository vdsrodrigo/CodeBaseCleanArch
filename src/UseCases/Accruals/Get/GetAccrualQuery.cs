using Application.Accruals.Create;
using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.Accruals.Get;

public record GetAccrualQuery(int AccrualId) : IQuery<Result<AccrualDTO>>;