using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch.Async.Abstractions
{
    public interface IAsyncExecutableRethrower<T, TResult> : IAsyncExecutable<T, TResult>
    {
        IAsyncExecutable<T, TResult> FinallyAsync(Func<T, Task> finallyAction);
    }
}
