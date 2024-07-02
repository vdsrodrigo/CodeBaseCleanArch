namespace Domain.UseCases.Accruals.List;

public interface IListAccrualsQueryService
{
    Task<IEnumerable<AccrualDTO>> ListAsync();
}