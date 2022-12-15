using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Net7.WebApi.Test.Data;
using Net7.WebApi.Test.Services.Abstractions;

namespace Net7.WebApi.Test.Services
{
    public class DbContextWrapper<T> : IDbContextWrapper<T> where T: DbContext
    {
        public DbContextWrapper(IDbContextFactory<T> dbContextFactory) 
        {
            DbContext = dbContextFactory.CreateDbContext();
        }

        public T DbContext { get; init; } = null!;

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
