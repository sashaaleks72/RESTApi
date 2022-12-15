using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Net7.WebApi.Test.Services.Abstractions
{
    public abstract class TransactionService<T> 
        where T: DbContext
    {
        private readonly IDbContextWrapper<T> _dbContextWrapper;

        protected TransactionService(IDbContextWrapper<T> dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
        }

        protected Task ExecuteSafeAsync(Func<Task> action, CancellationToken cancellationToken = default) => ExecuteSafeAsync(token => action(), cancellationToken);

        protected Task<TResult> ExecuteSafeAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default) => ExecuteSafeAsync(token => action(), cancellationToken);

        private async Task ExecuteSafeAsync(Func<CancellationToken, Task> Action, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

            try
            {
                await Action(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                Debug.WriteLine($"Transaction rollbacked with exception:\n {ex}");
                throw;
            }
        }

        private async Task<TResult> ExecuteSafeAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await action(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                Debug.WriteLine($"Transaction rollbacked with exception:\n {ex}");
                throw;
            }
        }
    }
}
