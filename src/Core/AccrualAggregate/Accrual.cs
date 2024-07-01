using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Domain.Shared;

namespace Domain.AccrualAggregate;

public class Accrual(string memberNumber, int amount, DateTime accrualDate, Partner partner)
    : EntityBase, IAggregateRoot
{
    public string MemberNumber { get; private set; } = Guard.Against.NullOrEmpty(memberNumber, nameof(memberNumber));
    public int Amount { get; private set; } = Guard.Against.NegativeOrZero(amount, nameof(amount));
    public DateTime AccrualDate { get; private set; } = accrualDate;
    public AccrualStatus AccrualStatus { get; private set; } = AccrualStatus.Pending;
    public DateTime ExpirationDate { get; private set; } = accrualDate.AddYears(1);
    public Partner Partner { get; private set; } = partner;

    public PhoneNumber? PhoneNumber { get; private set; }

    public void SetPhoneNumber(string phoneNumber)
    {
        PhoneNumber = new PhoneNumber(string.Empty, string.Empty, phoneNumber);
    }
}

public class PhoneNumber(
    string countryCode,
    string area,
    string number) : ValueObject
{
    public string CountryCode { get; private set; } = countryCode;
    public string Area { get; private set; } = area;
    public string Number { get; private set; } = number;


    public string GetFullNumber()
    {
        return $"{CountryCode}{Area}{Number}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
        yield return Area;
    }
}