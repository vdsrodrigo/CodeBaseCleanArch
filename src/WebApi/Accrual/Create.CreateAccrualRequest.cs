using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Shared;

namespace WebApi.Accrual;

public class CreateAccrualRequest
{
    public const string Route = "/Accrual";

    [Required] 
    public string MemberNumber { get; set; }
    [Required] 
    public int Amount { get; set; }
    [Required] 
    public Partner Partner { get; set; }
    [Required] 
    public DateTime AccrualDate { get; set; }
}