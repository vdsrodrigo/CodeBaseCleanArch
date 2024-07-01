using Ardalis.SharedKernel;
using Domain.Shared;

namespace Domain.AccrualAggregate;

public class Accrual(string memberNumber, int amount, DateTime accrualDate, Partner partner)
    : EntityBase, IAggregateRoot
{
    public string MemberNumber { get; private set; } = memberNumber;
    public int Amount { get; private set; } = amount;
    public DateTime AccrualDate { get; private set; } = accrualDate;
    public AccrualStatus AccrualStatus { get; private set; } = AccrualStatus.Pending;
    public DateTime ExpirationDate { get; private set; } = accrualDate.AddYears(1);
    public Partner Partner { get; private set; } = partner;
}