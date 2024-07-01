using System.ComponentModel.DataAnnotations;
using Domain.AccrualAggregate;
using Domain.Shared;

namespace WebApi.Accruals;

public struct CreateAccrualRequest
{
    public const string Route = "/Accrual";

    [Required] public string MemberNumber { get; set; }
    [Required] public int Amount { get; set; }
    [Required] public Partner Partner { get; set; }
    [Required] public DateTime AccrualDate { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
}