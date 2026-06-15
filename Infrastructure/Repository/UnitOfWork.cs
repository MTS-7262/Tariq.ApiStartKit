using Application.Abstractions.Data;
using Infrastructure.Database;

namespace Infrastructure.Repository;

public class UnitOfWork(ApplicationDbContext dataContext) : IUnitOfWork
{
    public int Commit()
    {
        return dataContext.SaveChanges();
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await dataContext.SaveChangesAsync(cancellationToken);
    }

    public void Rollback()
    {
        dataContext.Dispose();
    }

    public async Task RollbackAsync()
    {
        await dataContext.DisposeAsync();
    }
}
