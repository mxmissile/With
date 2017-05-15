using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RussellEast.DataAccessBuilder
{
    internal class ConnectionOptions : IConnectionOptions
    {
        private readonly Context context;

        private ICommandBuilder commandBuilder;

        public ConnectionOptions(Context context)
        {
            this.context = context;

            commandBuilder = new CommandBuilder(context);
        }
        
        public ICommandBuilder UsingConnectionString(string connectionString)
        {
            context.Connection = new SqlConnection(connectionString);

            return commandBuilder;
        }

        public ICommandBuilder UsingConfiguredConnection(string configKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[configKey].ConnectionString;
            
            return UsingConnectionString(connectionString);
        }

        public ICommandBuilder UsingConnection(IDbConnection connection)
        {
            context.IsSuppliedConnection = true;
            context.Connection = connection;

            return commandBuilder;
        }
    }
}