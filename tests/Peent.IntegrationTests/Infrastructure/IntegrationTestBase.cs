using System;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
//    public abstract class IntegrationTestBase  : IAsyncLifetime
//    {
//        private static readonly AsyncLock Mutex = new AsyncLock();
//
//        private static bool _initialized;
//        protected readonly RunAsContext BaseContext;
//
//        public virtual async Task InitializeAsync()
//        {
//            if (_initialized)
//                return;
//
//            using (await Mutex.LockAsync())
//            {
//                if (_initialized)
//                    return;
//                
//                await ResetCheckpoint();
////                BaseContext = await RunAsNewUserAsync();
//
//                _initialized = true;
//            }
//        }
//
//        protected IntegrationTestBase()
//        {
//            ResetCheckpoint().GetAwaiter().GetResult();
////            BaseContext = RunAsNewUserAsync().GetAwaiter().GetResult();
//        }
//
//        public virtual Task DisposeAsync() => Task.CompletedTask;
//    }

    public class IntegrationTest : IDisposable
    {
        public IntegrationTest()
        {
            ResetCheckpoint().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
//            ResetCheckpoint().GetAwaiter().GetResult();
        }
    }
}