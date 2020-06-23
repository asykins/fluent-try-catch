using FluentTryCatch.Abstractions;
using System;
using System.Collections.Generic;

namespace FluentTryCatch
{
    public class InitialCatcher<T, TResult> : ICatcherOrRethrower<T, TResult>
    {
        private readonly T _content;
        private readonly Func<T, TResult> _tryFunc;

        private readonly Dictionary<string, Action<T, Exception>> _catchActions;

        public InitialCatcher(T content, Func<T, TResult> tryFunc)
            => (_content, _tryFunc, _catchActions) = (content, tryFunc, new Dictionary<string, Action<T, Exception>>());

        public IExecutableCatcher<T, TResult> Catch<ExceptionType>(Action<T, Exception> catchAction)
        {
            if(!_catchActions.ContainsKey(typeof(ExceptionType).Name))
            {
                _catchActions.Add(typeof(ExceptionType).Name, catchAction);
            }

            return new ExecutableTryCatch<T, TResult>(this._content, this._tryFunc, this._catchActions);
        }

        public IExecutableRethrower<T, TResult> ReThrow()
            => new ExecutableTryCatch<T, TResult>(this._content, this._tryFunc, this._catchActions);
    }
}
