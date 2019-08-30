using System;
using System.IO;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peent.Api;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;
using Peent.Persistence;
using Respawn;

namespace Peent.IntegrationTests
{
    public class DatabaseFixture
    {
        public static Fixture F = new Fixture();
        private static readonly Checkpoint _checkpoint;
        private static readonly IConfigurationRoot _configuration;
        public static readonly IServiceScopeFactory _scopeFactory;

        static DatabaseFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

            var startup = new Startup(_configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetService<IServiceScopeFactory>();
            _checkpoint = new Checkpoint();
        }

        public static Task ResetCheckpoint() => _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

        public static async ValueTask ExecuteScopeAsync(Func<IServiceProvider, ValueTask> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<IApplicationDbContext>();
                try
                {
                    await db.BeginTransactionAsync().ConfigureAwait(false);

                    await action(scope.ServiceProvider).ConfigureAwait(false);

                    await db.CommitTransactionAsync().ConfigureAwait(false);
                }
                catch (Exception)
                {
                    db.RollbackTransaction();
                    throw;
                }
            }
        }

        public static async ValueTask<T> ExecuteScopeAsync<T>(Func<IServiceProvider, ValueTask<T>> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<IApplicationDbContext>();

                try
                {
                    await db.BeginTransactionAsync().ConfigureAwait(false);

                    var result = await action(scope.ServiceProvider).ConfigureAwait(false);

                    await db.CommitTransactionAsync().ConfigureAwait(false);

                    return result;
                }
                catch (Exception)
                {
                    db.RollbackTransaction();
                    throw;
                }
            }
        }

        public static ValueTask ExecuteDbContextAsync(Func<IApplicationDbContext, ValueTask> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<IApplicationDbContext>()));

        public static ValueTask ExecuteDbContextAsync(Func<IApplicationDbContext, IMediator, ValueTask> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<IApplicationDbContext>(), sp.GetService<IMediator>()));

        public static ValueTask<T> ExecuteDbContextAsync<T>(Func<IApplicationDbContext, ValueTask<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<IApplicationDbContext>()));

        public static ValueTask<T> ExecuteDbContextAsync<T>(Func<IApplicationDbContext, IMediator, ValueTask<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<IApplicationDbContext>(), sp.GetService<IMediator>()));

        public static ValueTask InsertAsync<T>(params T[] entities) where T : class
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    db.Set<T>().Add(entity);
                }
                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity, TEntity2>(TEntity entity, TEntity2 entity2)
            where TEntity : class
            where TEntity2 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);
                db.Set<TEntity2>().Add(entity2);

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity, TEntity2, TEntity3>(TEntity entity, TEntity2 entity2, TEntity3 entity3)
            where TEntity : class
            where TEntity2 : class
            where TEntity3 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);
                db.Set<TEntity2>().Add(entity2);
                db.Set<TEntity3>().Add(entity3);

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity, TEntity2, TEntity3, TEntity4>(TEntity entity, TEntity2 entity2, TEntity3 entity3, TEntity4 entity4)
            where TEntity : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);
                db.Set<TEntity2>().Add(entity2);
                db.Set<TEntity3>().Add(entity3);
                db.Set<TEntity4>().Add(entity4);

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask<T> FindAsync<T>(int id)
            where T : class
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync());
        }

        public static ValueTask<T> FindAsync<T>(long id)
            where T : class
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(id));
        }

        public static ValueTask<T> FindAsync<T>(string id)
            where T : class
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(id));
        }

        public static ValueTask<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return new ValueTask<TResponse>(mediator.Send(request));
            });
        }

        public static ValueTask SendAsync(IRequest request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return new ValueTask(mediator.Send(request));
            });
        }
    }
}
