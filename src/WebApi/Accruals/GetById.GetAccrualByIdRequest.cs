namespace WebApi.Accruals;

public class GetAccrualByIdRequest
{
    public const string Route = "/accruals/{AccrualId:int}";
    public static string BuildRoute(int accrualId) => Route.Replace("{AccrualId:int}", accrualId.ToString());
    
    public int AccrualId { get; set; }
}