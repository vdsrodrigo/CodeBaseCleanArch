using Domain.Shared;

namespace WebApi.Accruals;

public record AccrualRecord(int Id, string MemberNumber, int Amount, Partner Partner, string? PhoneNumber);