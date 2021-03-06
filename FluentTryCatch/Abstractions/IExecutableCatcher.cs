﻿using System;

namespace FluentTryCatch.Abstractions
{
    public interface IExecutableCatcher<T, TResult> : IExecutable<T, TResult>
    {
        IExecutableCatcher<T, TResult> Catch<ExceptionType>(Action<T, Exception> catchAction);

        IExecutable<T, TResult> Finally(Action<T> finallyAction);
    }
}
