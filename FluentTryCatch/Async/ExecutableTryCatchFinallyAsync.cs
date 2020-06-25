using FluentTryCatch.Async.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch.Async
{
    public class ExecutableTryCatchFinallyAsync<T, TResult> : IAsyncExecutable<T, TResult>
    {
        private readonly T _content;
        private readonly Func<T, Task<TResult>> _tryFuncAsync;
        private readonly Func<T, Task> _finallyAction;
        private readonly Dictionary<string, Func<T, Exception, Task>> _asyncCatchActions;

        public ExecutableTryCatchFinallyAsync
            (T content, 
             Func<T, Task<TResult>> tryFuncAsync, 
             Dictionary<string, Func<T, Exception, Task>> asyncCatchActions,
             Func<T, Task> finallyAction)
        {
            _content = content;
            _tryFuncAsync = tryFuncAsync;
            _asyncCatchActions = asyncCatchActions;
            _finallyAction = finallyAction;
        }

        public async Task<TResult> ExecuteAsync()
        {
            try
            {
                return await _tryFuncAsync(_content);
            }
            catch(Exception exception)
            {
                if(_asyncCatchActions == null || !_asyncCatchActions.Any())
                    throw;

                if(_asyncCatchActions.ContainsKey(exception.GetType().Name))
                {
                    await _asyncCatchActions.First(x => x.Key == exception.GetType().Name)
                        .Value(_content, exception);
                }
                else if(_asyncCatchActions.ContainsKey(typeof(Exception).Name))
                {
                    await _asyncCatchActions.First(x => x.Key == typeof(Exception).Name)
                        .Value(_content, exception);
                }

                return default;
            }
            finally
            {
                await _finallyAction(_content);
            }
        }
    }
}
