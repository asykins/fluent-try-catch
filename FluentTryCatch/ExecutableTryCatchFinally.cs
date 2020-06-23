using FluentTryCatch.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentTryCatch
{

    public class ExecutableTryCatchFinally<T, TResult> : IExecutable<T, TResult>
    {
        private readonly T _content;
        private readonly Func<T, TResult> _tryFunc;
        private readonly Dictionary<string, Action<T, Exception>> _catchActions;
        private readonly Action<T> _finallyAction;

        public ExecutableTryCatchFinally(T content,
                                         Func<T, TResult> tryFunc,
                                         Dictionary<string, Action<T, Exception>> catchActions,
                                         Action<T> finallyAction)
        {
            _content = content;
            _tryFunc = tryFunc;
            _catchActions = catchActions;
            _finallyAction = finallyAction;
        }

        public TResult Execute()
        {
            try
            {
                return _tryFunc(_content);
            }
            catch (Exception exception)
            {
                if (_catchActions == null || !_catchActions.Any())
                    throw;
                
                if (_catchActions.Any(x => x.Key == exception.GetType().Name))
                {
                    _catchActions.First(x => x.Key == exception.GetType().Name).Value(_content, exception);
                }
                else
                {
                    _catchActions.First(x => x.Key == typeof(Exception).Name).Value(_content, exception);
                }

                return default;
            }
            finally
            {
                _finallyAction(_content);
            }
        }
    }
}
