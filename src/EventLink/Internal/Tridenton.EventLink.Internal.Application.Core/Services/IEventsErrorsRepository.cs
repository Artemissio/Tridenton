namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsErrorsRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    ValueTask PersistErrorAsync(Error error);
}