using FluentTryCatch.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTryCatch
{
    public static class Trier
    {
        public static ICatcherOrRethrower<T, TResult> Try<T, TResult>(T content, Func<T, TResult> tryAction)
            => new Trier<T, TResult>(content).Try(tryAction);
    }
}
