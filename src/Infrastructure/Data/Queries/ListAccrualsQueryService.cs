using Domain.UseCases.Accruals;
using Domain.UseCases.Accruals.List;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Queries;

public class ListAccrualsQueryService(AppDbContext db) : IListAccrualsQueryService
{
    // Você pode usar EF, Dapper, SqlClient e etc para queries
    // isto é apenas um exemplo

    public async Task<IEnumerable<AccrualDTO>> ListAsync()
    {
        // NOTA: Isto falhará se testar com o provider EF InMemory!
        var result = await db.Database.SqlQuery<AccrualDTO>(
                $"SELECT Id, member_number as MemberNumber, amount, 1 as partner, phone_number as PhoneNumber FROM Accruals")
            .ToListAsync();

        return result;
    }
}