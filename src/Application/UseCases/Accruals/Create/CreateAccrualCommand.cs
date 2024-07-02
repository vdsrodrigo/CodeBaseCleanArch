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
public record CreateAccrualCommand(string MemberNumber, int Amount, DateTime AccrualDate, Partner Partner, string? PhoneNumber) : ICommand<Result<int>>;