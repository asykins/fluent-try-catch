using System;

namespace FluentTryCatch.Abstractions
{
    public interface IExecutableRethrower<T, TResult> : IExecutable<T, TResult>
    {
        IExecutable<T, TResult> Finally(Action<T> finallyAction);
    }
}
