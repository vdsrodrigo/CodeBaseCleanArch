using System.ComponentModel.DataAnnotations;
using Domain.AccrualAggregate;
using Domain.Shared;

namespace WebApi.Accruals;

public struct CreateAccrualRequest
{
    public const string Route = "/accrual";

    public string MemberNumber { get; set; }
    public int Amount { get; set; }
    public Partner Partner { get; set; }
    public DateTime AccrualDate { get; set; }
    public string PhoneNumber { get; set; }
}