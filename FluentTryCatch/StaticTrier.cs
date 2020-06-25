using FluentTryCatch.Abstractions;
using FluentTryCatch.Async.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch
{
    public static class Trier
    {
        public static ICatcherOrRethrower<T, TResult> Try<T, TResult>(T content, Func<T, TResult> tryAction)
            => new Trier<T, TResult>(content).Try(tryAction);

        public static IAsyncCatcherOrRethrower<T, TResult> TryAsync<T, TResult>(T content, Func<T, Task<TResult>> asyncTryFunction)
            => new Trier<T, TResult>(content).TryAsync(asyncTryFunction);
    }
}
