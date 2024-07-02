using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebApi.Accruals;

public class UpdateAccrualRequest
{
    public const string Route  = "/accruals/{accrualId:int}";
    public static string BuildRoute(int accrualId) => Route.Replace("{accrualId:int}", accrualId.ToString());
    
    public int AccrualId { get; set; }
    [Required]
    public int Id { get; set; }
    [Required]
    public int Amount { get; set; }
}