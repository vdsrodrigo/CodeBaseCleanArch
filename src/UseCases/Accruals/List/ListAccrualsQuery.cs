using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Application.Accruals.List;

public record ListAccrualsQuery(int? skip, int? take) : IQuery<Result<IEnumerable<AccrualDTO>>>;