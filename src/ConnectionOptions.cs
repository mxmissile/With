using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RussellEast.DataAccessBuilder
{
    internal class ConnectionOptions : IConnectionOptions
    {
        private readonly Context _context;

        private readonly ICommandBuilder _commandBuilder;

        public ConnectionOptions(Context context)
        {
            _context = context;

            _commandBuilder = new CommandBuilder(context);
        }
        
        public ICommandBuilder UsingConnectionString(string connectionString)
        {
            _context.Connection = new SqlConnection(connectionString);

            return _commandBuilder;
        }

        public ICommandBuilder UsingConfiguredConnection(string configKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[configKey].ConnectionString;
            
            return UsingConnectionString(connectionString);
        }

        public ICommandBuilder UsingConnection(IDbConnection connection)
        {
            _context.IsSuppliedConnection = true;
            _context.Connection = connection;

            return _commandBuilder;
        }
    }
}