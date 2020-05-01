﻿using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Peent.Application;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Common;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;
using Peent.Domain.ValueObjects;

namespace Peent.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>,
        IApplicationDbContext
    {
        private IDbContextTransaction _currentTransaction;
        private readonly IUserAccessor _userAccessor;

        public ApplicationDbContext(
            DbContextOptions options,
            IUserAccessor userAccessor)
            : base(options)
        {
            _userAccessor = userAccessor;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IHaveAuditInfo>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    {
                        entry.Entity.SetCreatedBy(_userAccessor.User.GetUser());
                        Entry(entry.Entity.Created.By).State = EntityState.Unchanged;
                    }
                        break;
                    case EntityState.Modified:
                    {
                        entry.Entity.SetModifiedBy(_userAccessor.User.GetUser());
                        Entry(entry.Entity.LastModified.By).State = EntityState.Unchanged;
                    }
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction =
                await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}