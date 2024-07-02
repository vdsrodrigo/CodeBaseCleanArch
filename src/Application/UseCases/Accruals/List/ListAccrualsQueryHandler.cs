using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Domain.UseCases.Accruals.List;

public class ListAccrualsQueryHandler(IListAccrualsQueryService query) : IQueryHandler<ListAccrualsQuery, Result<IEnumerable<AccrualDTO>>>
{
    public async Task<Result<IEnumerable<AccrualDTO>>> Handle(ListAccrualsQuery request, CancellationToken cancellationToken)
    {
        var result = await query.ListAsync();
        return  Result.Success(result);    }
}