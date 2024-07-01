using Domain.Shared;

namespace Application.Accruals;

public record AccrualDTO(int Id, string MemberNumber, int Amount, Partner Partner, string? PhoneNumber);