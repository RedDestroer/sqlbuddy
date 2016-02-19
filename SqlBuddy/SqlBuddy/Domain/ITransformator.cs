namespace SqlBuddy.Domain
{
    /// <summary>
    /// Refines the structure of <see cref="SqlDatabaseDefinition"/> with rules build in this transformator
    /// </summary>
    public interface ITransformator
    {
        void Transform(SqlDatabaseDefinition sqlDatabaseDefinition);
    }
}