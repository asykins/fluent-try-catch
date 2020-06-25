using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch.Async.Abstractions
{
    public interface IAsyncCatcherOrRethrower<T, TResult> : IAsyncCatcher<T, TResult>, IAsyncRethrower<T, TResult>
    {
        
    }
}
