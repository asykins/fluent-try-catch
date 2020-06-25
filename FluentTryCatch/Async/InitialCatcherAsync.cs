using FluentTryCatch.Async.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch.Async
{
    public class InitialCatcherAsync<T, TResult> : IAsyncCatcherOrRethrower<T, TResult>
    {
        private readonly T _content;
        private readonly Func<T, Task<TResult>> _tryFuncAsync;

        private readonly Dictionary<string, Func<T, Exception, Task>> _asyncCatchActions;

        public InitialCatcherAsync(T content, Func<T, Task<TResult>> tryFuncAsync)
            => (_content, _tryFuncAsync, _asyncCatchActions) = (content, tryFuncAsync, new Dictionary<string, Func<T, Exception, Task>>());

        public IAsyncExecutableCatcher<T, TResult> CatchAsync<ExceptionType>(Func<T, Exception, Task> catchAction)
        {
            if (!_asyncCatchActions.ContainsKey(typeof(ExceptionType).Name))
            {
                _asyncCatchActions.Add(typeof(ExceptionType).Name, catchAction);
            }

            return new ExecutableTryCatchAsync<T, TResult>(this._content, this._tryFuncAsync, this._asyncCatchActions);
        }

        public IAsyncExecutableRethrower<T, TResult> ReThrow()
            => new ExecutableTryCatchAsync<T, TResult>(this._content, _tryFuncAsync, this._asyncCatchActions);
    }
}
