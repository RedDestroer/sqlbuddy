using SqlBuddy.Domain;

namespace SqlBuddy.VisualStudio
{
    public interface IDatabaseProvider
    {
        DatabaseDefinition DatabaseDefinition { get; }
        string Namespace { get; }
        SqlDatabaseDefinition SqlDatabaseDefinition { get; }
    }
}