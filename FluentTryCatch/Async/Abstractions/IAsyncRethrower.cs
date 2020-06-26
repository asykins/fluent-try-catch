using FluentTryCatch.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch.Async.Abstractions
{
    public interface IAsyncRethrower<T, TResult>
    {
        IAsyncExecutableRethrower<T, TResult> ReThrow();
    }
}
