namespace Tridenton.EventLink.Internal.Application.Core.Services;

public class RelationalDatabaseCommandParser : ISourceCommandParser
{
    public ValueTask<Result<SourceCommandParsedPayload>> ParseAsync(SourceCommand command)
    {
        var segments = command.CommandSegments;

        var statement = segments[0];

        throw new NotImplementedException();
    }
}