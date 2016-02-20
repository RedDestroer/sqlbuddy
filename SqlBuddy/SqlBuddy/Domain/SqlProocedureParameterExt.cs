namespace SqlBuddy.Domain
{
    public class SqlProocedureParameterExt
    {
        public SqlProocedureParameterExt(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsNullable { get; set; }
        public string Description { get; set; }
    }
}