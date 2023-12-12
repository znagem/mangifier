namespace Mangifier.Api.Shared.IocFactory;

public sealed class AbstractFactory<T>(Func<T> factory) : IAbstractFactory<T>
{
    public T Create() => factory();
}