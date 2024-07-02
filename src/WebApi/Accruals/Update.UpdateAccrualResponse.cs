namespace WebApi.Accruals;

public class UpdateAccrualResponse(AccrualRecord accrual)
{
    public AccrualRecord Accrual { get; set; } = accrual;
}