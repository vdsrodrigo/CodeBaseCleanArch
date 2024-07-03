using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.Shared;

namespace Domain.UseCases.Accruals.Create;

/// <summary>
/// Cria um novo acúmulo
/// </summary>
/// <param name="MemberName"></param>
/// <param name="Amount"></param>
/// <param name="Partner"></param>
/// <param name="PhoneNumber"></param>
public class CreateAccrualCommand(
    string memberNumber,
    int amount,
    DateTime accrualDate,
    Partner partner,
    string? phoneNumber)
    : ICommand<Result<int>>
{
    public string MemberNumber { get; } = memberNumber;
    public int Amount { get; } = amount;
    public DateTime AccrualDate { get; } = accrualDate;
    public Partner Partner { get; } = partner;
    public string? PhoneNumber { get; } = phoneNumber;
}