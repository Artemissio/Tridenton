namespace Tridenton.Core.Utilities;

public interface IUnique<TKey>
    where TKey : struct, IEquatable<TKey>, IComparable<TKey>
{
    TKey Id { get; init; }
}

public interface IUlidUnique : IUnique<Ulid> { }
public interface IGuidUnique : IUnique<Guid> { }
public interface IIndexUnique : IUnique<int> { }