using Domain.Shared;

namespace Domain.UseCases.Accruals;

public record AccrualDTO(int Id, string MemberNumber, int Amount, Partner Partner, string? PhoneNumber);