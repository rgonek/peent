using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using FakeItEasy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peent.Application.Categories.Queries.GetCategory;
using Peent.Application.Infrastructure;
using Peent.Application.Interfaces;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;
using Peent.Persistence;
using Respawn;

namespace Peent.IntegrationTests.Infrastructure
{
    public class DatabaseFixture
    {
        public static readonly Fixture F = new Fixture();
        private static readonly Checkpoint _checkpoint;
        private static readonly IConfigurationRoot _configuration;
        private static readonly IServiceScopeFactory _scopeFactory;
        public static readonly IUserAccessor UserAccessor;

        static DatabaseFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            UserAccessor = A.Fake<IUserAccessor>();

            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetService<IServiceScopeFactory>();
            _checkpoint = new Checkpoint();

            F.Configure();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped(sp => UserAccessor);

            services.AddMediatR(typeof(GetCategoryQueryHandler));
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
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(id));
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

        public static async Task<ApplicationUser> CreateUserAsync()
        {
            var user = F.Create<ApplicationUser>();
            await InsertAsync(user);

            return user;
        }

        public static async Task<Workspace> CreateWorkspaceAsync(ApplicationUser user)
        {
            var workspace = new Workspace
            {
                CreationDate = Clock.UtcNow,
                CreatedById = user.Id
            };
            await InsertAsync(workspace);

            return workspace;
        }

        public static ClaimsPrincipal SetCurrentUser(ApplicationUser user, Workspace workspace)
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(KnownClaims.WorkspaceId, workspace.Id.ToString()),
            }, "mock"));
            A.CallTo(() => UserAccessor.User).Returns(claimsPrincipal);

            return claimsPrincipal;
        }
    }
}
