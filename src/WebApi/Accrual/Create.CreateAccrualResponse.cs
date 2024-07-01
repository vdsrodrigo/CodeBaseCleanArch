namespace WebApi.Accrual;

public class CreateAccrualResponse(int id, string memberNumber)
{
    public int Id { get; set; } = id;
    public string MemberNumber { get; set; } = memberNumber;
}