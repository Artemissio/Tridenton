namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Utilities;

internal readonly struct SQLScripts
{
    public const string GetAllTablesCommandTemplate = """
        SELECT * FROM pg_tables
        WHERE schemaname = '{0}';
    """;
    
    public const string CreateChangesTableCommand = """
        CREATE TABLE IF NOT EXISTS {0}.tridenton_event_link_changes
        (
            id UUID PRIMARY KEY,
            data JSONB NOT NULL,
            occured_on_utc TIMESTAMPTZ NOT NULL,
            processed_on_utc TIMESTAMPTZ NULL,
            error TEXT NULL
        );

        CREATE INDEX IF NOT EXISTS idx_tridenton_event_link_changes_unprocessed
        ON {0}.tridenton_event_link_changes (occured_on_utc, processed_on_utc)
        INCLUDE (id, data)
        WHERE processed_on_utc IS NULL;
    """;
    
    public const string CreateChangesLoggingFunctionCommand = """
        CREATE OR REPLACE FUNCTION {0}.log_tridenton_event_link_changes()
        RETURNS TRIGGER
        AS $$
        BEGIN
            INSERT INTO {0}.tridenton_event_link_changes(id, data, occured_on_utc)
            VALUES
            (
                gen_random_uuid(),
                jsonb_build_object(
                        'Table', TG_TABLE_NAME,
                        'Command', TG_OP,
                        'OldRecord', CASE WHEN TG_OP IN ('UPDATE', 'DELETE') THEN to_jsonb(OLD) ELSE NULL END,
                        'NewRecord', CASE WHEN TG_OP IN ('INSERT', 'UPDATE') THEN to_jsonb(NEW) ELSE NULL END
                ),
                now()
            );
        
            RETURN NULL;
        END;
        $$ LANGUAGE plpgsql;
    """;
}