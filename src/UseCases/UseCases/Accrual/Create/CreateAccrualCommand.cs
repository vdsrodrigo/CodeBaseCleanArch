using Ardalis.Result;
using Ardalis.SharedKernel;
using Domain.Shared;

namespace Application.UseCases.Accrual.Create;

/// <summary>
/// Cria um novo acúmulo
/// </summary>
/// <param name="MemberName"></param>
/// <param name="Amount"></param>
/// <param name="Partner"></param>
public record CreateAccrualCommand(string MemberNumber, int Amount, DateTime AccrualDate, Partner Partner) : ICommand<Result<int>>;