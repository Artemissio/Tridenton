CREATE TABLE audit_log (
   id SERIAL PRIMARY KEY,
   table_name TEXT,
   operation TEXT,
   row_data JSONB,  -- Stores both OLD and NEW data
   created_at TIMESTAMP DEFAULT NOW()
);

-- Step 3: Create a trigger function for INSERT, UPDATE, and DELETE
CREATE OR REPLACE FUNCTION log_audit_trigger()
RETURNS TRIGGER
AS $$
BEGIN
    IF TG_OP = 'INSERT'
    THEN
        INSERT INTO audit_log (table_name, operation, row_data)
        VALUES
        (
            TG_TABLE_NAME,
            'INSERT',
            jsonb_build_object('new', to_jsonb(NEW))  -- Store NEW only
        );

    ELSIF TG_OP = 'UPDATE'
    THEN
        INSERT INTO audit_log (table_name, operation, row_data)
        VALUES
        (
            TG_TABLE_NAME,
            'UPDATE',
            jsonb_build_object('old', to_jsonb(OLD), 'new', to_jsonb(NEW))  -- Store both OLD and NEW
        );

    ELSIF TG_OP = 'DELETE'
    THEN
        INSERT INTO audit_log (table_name, operation, row_data)
        VALUES
        (
            TG_TABLE_NAME,
            'DELETE',
            jsonb_build_object('old', to_jsonb(OLD))  -- Store OLD only
        );
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;