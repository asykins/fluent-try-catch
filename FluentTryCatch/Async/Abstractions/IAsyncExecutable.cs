using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch.Async.Abstractions
{
    public interface IAsyncExecutable<T, TResult>
    {
        Task<TResult> ExecuteAsync();
    }
}
