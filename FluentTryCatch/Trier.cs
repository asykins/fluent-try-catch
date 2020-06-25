using FluentTryCatch.Abstractions;
using FluentTryCatch.Async;
using FluentTryCatch.Async.Abstractions;
using System;
using System.Threading.Tasks;

namespace FluentTryCatch
{

    public class Trier<T, TResult>
    {
        private readonly T _content;

        private static Func<T, TResult> NullTry
            => (T content) => throw new ArgumentNullException(nameof(content));

        private static Func<T, Task<TResult>> NullTryAsync
            => async (T content) => { await Task.Yield();
                throw new ArgumentNullException(nameof(content));
            };

        public Trier(T content)
            => (_content) = (content);

        public ICatcherOrRethrower<T, TResult> Try(Func<T, TResult> tryFunc)
        {
            if (_content == null)
            {
                return new InitialCatcher<T, TResult>(_content, NullTry);
            }
            else
            {
                return new InitialCatcher<T, TResult>(_content, tryFunc);
            }
        }

        public IAsyncCatcherOrRethrower<T, TResult> TryAsync(Func<T, Task<TResult>> asyncTryAction)
        {
            if(_content == null)
            {
                return new InitialCatcherAsync<T, TResult>(_content, NullTryAsync);
            }
            else
            {
                return new InitialCatcherAsync<T, TResult>(_content, asyncTryAction);
            }
        }
    }
}
