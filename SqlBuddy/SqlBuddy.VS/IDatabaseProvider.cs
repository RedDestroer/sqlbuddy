using SqlBuddy.Domain;

namespace SqlBuddy.VisualStudio
{
    public interface IDatabaseProvider
    {
        DataAccess DataAccess { get; }
        string Namespace { get; }
        SqlDatabaseDefinition SqlDatabaseDefinition { get; }
    }
}