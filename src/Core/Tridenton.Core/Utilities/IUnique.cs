namespace Tridenton.Core.Utilities;

public interface IUnique<TKey> where TKey : struct
{
    TKey Id { get; }
}

public interface IUlidUnique : IUnique<Ulid> { }
public interface IGuidUnique : IUnique<Guid> { }