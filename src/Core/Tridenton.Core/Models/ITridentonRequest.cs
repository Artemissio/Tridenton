namespace Tridenton.Core;

/// <summary>
/// Marker interface
/// </summary>
public interface ITridentonRequest;

/// <summary>
/// Marker interface
/// </summary>
public interface ITridentonRequest<out TResponse> where TResponse : class;