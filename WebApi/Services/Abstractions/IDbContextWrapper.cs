using Microsoft.EntityFrameworkCore.Storage;

namespace Net7.WebApi.Test.Services.Abstractions
{
    public interface IDbContextWrapper<TContext>
    {
        public TContext DbContext { get; init; }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
