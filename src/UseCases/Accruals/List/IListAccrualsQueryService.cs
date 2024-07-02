namespace Application.Accruals.List;

public interface IListAccrualsQueryService
{
    Task<IEnumerable<AccrualDTO>> ListAsync();
}