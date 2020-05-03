using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;

namespace Peent.Application
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Workspace> Workspaces { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}
