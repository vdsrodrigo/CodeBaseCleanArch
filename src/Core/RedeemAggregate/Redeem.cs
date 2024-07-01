using Ardalis.SharedKernel;
using Domain.Shared;

namespace Domain.RedeemAggregate;

public class Redeem(string memberNumber, int amount, DateTime redeemDate, int redeemStatus, Partner partner)
    : EntityBase, IAggregateRoot
{
    public string MemberNumber { get; private set; } = memberNumber;
    public int Amount { get; private set; } = amount;
    public DateTime RedeemDate { get; private set; } = redeemDate;
    public int RedeemStatus { get; private set; } = redeemStatus;
    public DateTime ExpirationDate { get; private set; } = redeemDate.AddYears(1);
    public Partner Partner { get; private set; } = partner;
}