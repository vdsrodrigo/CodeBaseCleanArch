using Ardalis.Specification;

namespace Domain.AccrualAggregate.Specifications;

public class AccrualByIdSpec : Specification<Accrual>
{
    public AccrualByIdSpec(int accrualId)
    {
        Query.Where(accrual => accrual.Id == accrualId);
    }
}