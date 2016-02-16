namespace SqlBuddy.Dal
{
    public static class Sql
    {
        public static string Use(string databaseName)
        {
            return "USE " + databaseName;
        }
        
        public static string SelectAllSchemas()
        {
            return "SELECT schema_id, name FROM sys.schemas order by name";
        }
        
        public static string SelectProceduresForSchema(int shemaId)
        {
            return @"SELECT 
                    p.object_id ObjectId,
                    p.name ProcedureName,
                    s.name SchemaName
                FROM sys.procedures p
                    inner join sys.schemas s
                    on p.schema_id = s.schema_id
                WHERE p.is_ms_shipped = 0 and s.schema_id = " + shemaId + " AND p.Type = 'P' ORDER BY p.name";
        }
        
        public static string GetProcedureText(int procedureId)
        {
            return @"SELECT [text] FROM syscomments WHERE id = " + procedureId + " ORDER BY colid";
        }
    }
}