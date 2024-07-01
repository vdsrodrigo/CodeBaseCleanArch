using Ardalis.SharedKernel;
using Domain.AccrualAggregate;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher? dispatcher)
    : DbContext(options)
{
    public DbSet<Accrual> Accruals => Set<Accrual>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccrualConfiguration).Assembly);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        //Ignorar eventos se nenhum dispatcher for fornecido
        if (dispatcher == null) return result;
        
        //Despachar eventos apenas se o salvamento for bem-sucedido
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();
        
        await dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        
        return result;
    }
    
    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
}