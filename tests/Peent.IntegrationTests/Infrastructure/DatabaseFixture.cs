using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peent.Application;
using Peent.Application.Categories.Queries.GetCategory;
using Peent.CommonTests.Infrastructure;
using Peent.Domain.Common;
using Peent.Domain.Entities;
using Peent.Persistence;
using System.Linq.Dynamic.Core;
using Moq;
using Peent.Application.Common;
using Peent.Application.Common.DynamicQuery.Sorts;
using Respawn;

namespace Peent.IntegrationTests.Infrastructure
{
    public class DatabaseFixture : TestFixture
    {
        private static readonly Checkpoint Checkpoint;
        private static readonly IConfigurationRoot Configuration;
        public static readonly IServiceScopeFactory ScopeFactory;
        private static ClaimsPrincipal _currentUser;

        static DatabaseFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);
            ScopeFactory = services.BuildServiceProvider()
                .GetService<IServiceScopeFactory>();
            Checkpoint = new Checkpoint
            {
                TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory"
                }
            };

            EnsureDatabase().GetAwaiter().GetResult();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IUserAccessor));
            services.Remove(currentUserServiceDescriptor);
            services.AddTransient(provider => Mock.Of<IUserAccessor>(s => s.User == _currentUser));

            services.AddMediatR(typeof(GetCategoryQueryHandler));
        }

        public static async Task ResetState()
        {
            await Checkpoint.Reset(Configuration.GetConnectionString("DefaultConnection"));
            _currentUser = null;
        }

        private static async Task EnsureDatabase()
        {
            using var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
        }

        public static async ValueTask ExecuteScopeAsync(Func<IServiceProvider, ValueTask> action)
        {
            using var scope = ScopeFactory.CreateScope();
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

        public static async ValueTask<T> ExecuteScopeAsync<T>(Func<IServiceProvider, ValueTask<T>> action)
        {
            using var scope = ScopeFactory.CreateScope();
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
                    db.Set<T>().Attach(entity).State = EntityState.Added;
                }

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Attach(entity).State = EntityState.Added;

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity, TEntity2>(TEntity entity, TEntity2 entity2)
            where TEntity : class
            where TEntity2 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Attach(entity).State = EntityState.Added;
                db.Set<TEntity2>().Attach(entity2).State = EntityState.Added;

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity, TEntity2, TEntity3>(TEntity entity, TEntity2 entity2,
            TEntity3 entity3)
            where TEntity : class
            where TEntity2 : class
            where TEntity3 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Attach(entity).State = EntityState.Added;
                db.Set<TEntity2>().Attach(entity2).State = EntityState.Added;
                db.Set<TEntity3>().Attach(entity3).State = EntityState.Added;

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask InsertAsync<TEntity, TEntity2, TEntity3, TEntity4>(TEntity entity, TEntity2 entity2,
            TEntity3 entity3, TEntity4 entity4)
            where TEntity : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Attach(entity).State = EntityState.Added;
                db.Set<TEntity2>().Attach(entity2).State = EntityState.Added;
                db.Set<TEntity3>().Attach(entity3).State = EntityState.Added;
                db.Set<TEntity4>().Attach(entity4).State = EntityState.Added;

                return new ValueTask(db.SaveChangesAsync());
            });
        }

        public static ValueTask<T> FindAsync<T>(params object[] keyValues)
            where T : class
        {
            return ExecuteDbContextAsync(async db =>
            {
                if (typeof(IEntity<>).IsAssignableFromRawGeneric(typeof(T)))
                {
                    return await db.Set<T>()
                        .Include(db.GetIncludePaths<T>())
                        .Where($"{nameof(IEntity<int>.Id)} == @0", keyValues)
                        .SingleOrDefaultAsync();
                }

                return await db.Set<T>().FindAsync(keyValues);
            });
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

        public static async Task<ApplicationUser> CreateUserAsync()
        {
            var user = F.Create<ApplicationUser>();
            await InsertAsync(user);

            return user;
        }

        public static async Task<Workspace> CreateWorkspaceAsync()
        {
            var workspace = new Workspace();
            await InsertAsync(workspace);

            return workspace;
        }

        private static void SetCurrentUser(ApplicationUser user, Workspace workspace = null)
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(KnownClaims.WorkspaceId, workspace is null ? "" : workspace.Id.ToString())
            }, "mock"));
        }

        public static async Task<RunAsContext> RunAsNewUserAsync(Workspace workspace = null)
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user);

            workspace ??= await CreateWorkspaceAsync();

            return RunAs(user, workspace);
        }

        public static void RunAs(RunAsContext context)
        {
            SetCurrentUser(context.User, context.Workspace);
        }

        public static RunAsContext RunAs(ApplicationUser user, Workspace workspace)
        {
            var context = new RunAsContext(user, workspace);

            RunAs(context);

            return context;
        }
    }
}