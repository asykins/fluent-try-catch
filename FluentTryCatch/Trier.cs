using FluentTryCatch.Abstractions;
using System;

namespace FluentTryCatch
{

    public class Trier<T, TResult>
    {
        private readonly T _content;

        private static Func<T, TResult> NullTry
            => (T content) => throw new ArgumentNullException(nameof(content));

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
    }
}
