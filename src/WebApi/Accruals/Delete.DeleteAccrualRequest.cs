namespace WebApi.Accruals;

public record DeleteAccrualRequest
{
    public const string Route = "/Accruals/{AccrualId:int}";
    
    public static string BuildRoute(int accrualId) => Route.Replace("{AccrualId:int}", accrualId.ToString());
    
    public int AccrualId { get; set; }
}