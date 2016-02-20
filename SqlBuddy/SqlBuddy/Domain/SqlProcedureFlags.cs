namespace SqlBuddy.Domain
{
    public class SqlProcedureFlags
    {
        public SqlProcedureFlags(int row, int column)
        {
            Row = row;
            Column = column;
            IsPublic = true;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsPublic { get; set; }
    }
}