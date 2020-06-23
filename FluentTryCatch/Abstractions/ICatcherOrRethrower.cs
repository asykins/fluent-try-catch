namespace FluentTryCatch.Abstractions
{
    public interface ICatcherOrRethrower<T, TResult> : ICatcher<T, TResult>, IRethrower<T, TResult>
    {

    }
}
