namespace Tridenton.EventLink.Internal.Application.Core.Services;

public interface ISourceCommandParser
{
    ValueTask<Result<SourceCommandParsedPayload>> ParseAsync(SourceCommand command);
}