using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Tridenton.Core.Operations;

public abstract class DatabaseWriteOperation<TDbContext> : Operation, IAsyncDisposable
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;

    private IDbContextTransaction? _transaction;

    protected DatabaseWriteOperation(TDbContext dbContext, string name = "") : base(name)
    {
        DbContext = dbContext;
    }

    protected sealed override async ValueTask<Result> ExecuteCoreAsync(CancellationToken cancellationToken = default)
    {
        _transaction = DbContext.Database.CurrentTransaction ??
            await DbContext.Database.BeginTransactionAsync(cancellationToken);

        await WriteToDbAsync(cancellationToken);

        await _transaction.CommitAsync(cancellationToken);

        return Result.Success;
    }

    protected sealed override async ValueTask<Result> RollbackCoreAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
        }

        return Result.Success;
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }

    protected abstract ValueTask WriteToDbAsync(CancellationToken cancellationToken = default);
}