using System;

namespace FluentTryCatch.Abstractions
{
    public interface ICatcher<T, TResult>
    {
        IExecutableCatcher<T, TResult> Catch<ExceptionType>(Action<T, Exception> catchAction);
    }
}
