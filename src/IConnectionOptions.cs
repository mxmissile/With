using System.Data;

namespace RussellEast.DataAccessBuilder
{
    public interface IConnectionOptions
    {
        ICommandBuilder UsingConnectionString(string connectionString);
        ICommandBuilder UsingConfiguredConnection(string configKey);
        ICommandBuilder UsingConnection(IDbConnection connection);
    }
}