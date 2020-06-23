namespace FluentTryCatch.Abstractions
{
    public interface IExecutable<T, TResult>
    {
        TResult Execute();
    }
}
