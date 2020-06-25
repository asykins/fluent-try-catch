using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch.Async.Abstractions
{
    public interface IAsyncExecutableCatcher<T, TResult> : IAsyncExecutable<T, TResult>
    {
        IAsyncExecutableCatcher<T, TResult> CatchAsync<ExceptionType>(Func<T, Exception, Task> catchAction);
        IAsyncExecutable<T, TResult> FinallyAsync(Func<T, Task> finallyAction);
    }
}
