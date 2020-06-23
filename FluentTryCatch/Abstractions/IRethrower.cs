namespace FluentTryCatch.Abstractions
{
    public interface IRethrower<T, TResult>
    {
        IExecutableRethrower<T, TResult> ReThrow();
    }
}
