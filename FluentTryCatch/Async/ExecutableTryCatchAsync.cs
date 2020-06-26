using FluentTryCatch.Abstractions;
using FluentTryCatch.Async.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FluentTryCatch.Async
{
    public class ExecutableTryCatchAsync<T, TResult> : IAsyncExecutableCatcher<T, TResult>, IAsyncExecutableRethrower<T, TResult>
    {
        private readonly T _content;
        private readonly Func<T, Task<TResult>> _tryFuncAsync;

        private readonly Dictionary<string, Func<T, Exception, Task>> _asyncCatchActions;

        public ExecutableTryCatchAsync(T content, Func<T, Task<TResult>> tryFuncAsync, Dictionary<string, Func<T, Exception, Task>> asyncCatchActions)
        {
            _content = content;
            _tryFuncAsync = tryFuncAsync;
            _asyncCatchActions = asyncCatchActions;
        }

        public IAsyncExecutableCatcher<T, TResult> CatchAsync<ExceptionType>(Func<T, Exception, Task> catchAction)
        {
            _asyncCatchActions.Add(typeof(Exception).Name, catchAction);
            return this;
        }

        public IAsyncExecutable<T, TResult> FinallyAsync(Func<T, Task> finallyAction)
            => new ExecutableTryCatchFinallyAsync<T, TResult>
                (_content, _tryFuncAsync, _asyncCatchActions, finallyAction);

        public Task<TResult> ExecuteAsync()
            => new ExecutableTryCatchFinallyAsync<T, TResult>
                (_content, _tryFuncAsync, _asyncCatchActions, async (T content) => await Task.Run(() => {}))
                    .ExecuteAsync();
    }
}
