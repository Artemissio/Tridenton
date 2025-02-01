namespace Tridenton.EventLink.Internal.Application.Core.Services;

public class RelationalDatabaseEventTypeDeterminator : IEventTypeDeterminator
{
    public ValueTask<EventType> DetermineAsync(SourceCommand command)
    {
        var statement = command.CommandSegments[0];
        
        var relationalDbStatement = RelationalDatabaseHelpers.Statements
            .FirstOrDefault(s => string.Equals(s.Statement, statement, StringComparison.OrdinalIgnoreCase));

        if (relationalDbStatement is null)
        {
            return new ValueTask<EventType>(EventType.None);
        }
        
        return ValueTask.FromResult(relationalDbStatement.EventType);
    }
}