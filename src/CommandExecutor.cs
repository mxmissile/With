using System.Data;
using System.Data.SqlClient;
using NLog;

namespace RussellEast.DataAccessBuilder
{
    internal class CommandExecutor : ICommandExecutor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Context _context;

        public CommandExecutor(Context context)
        {
            _context = context;
        }

        public void ExecuteNonQuery()
        {
            try
            {
                BuildCommand().ExecuteNonQuery();
            }
            finally
            {
                if (!_context.IsSuppliedConnection && _context.Connection.State == ConnectionState.Open)
                {
                    _context.Connection.Close();
                }
            }
        }
        
        public T ExecuteScalar<T>()
        {
            try
            {
                return (T)BuildCommand().ExecuteScalar();
            }
            finally
            {
                if (!_context.IsSuppliedConnection && _context.Connection.State == ConnectionState.Open)
                {
                    _context.Connection.Close();
                }
                else
                {
                    Logger.Warn($"Unable to close connection, State: {_context.Connection.State}, Connection supplied: {_context.IsSuppliedConnection}");
                }
            }
        }

        public DynamicDbDataReader ExecuteReader()
        {
            var command = BuildCommand();

            return new DynamicDbDataReader((SqlDataReader)command.ExecuteReader(CommandBehavior.CloseConnection));
        }

        public DynamicDbDataReader ExecuteReader(CommandBehavior commandBehavior)
        {
            var command = BuildCommand();

            return new DynamicDbDataReader((SqlDataReader)command.ExecuteReader(commandBehavior));
        }
        
        private IDbCommand BuildCommand()
        {
            using (SqlCommand command = new SqlCommand(_context.CommandText, (SqlConnection) _context.Connection))
            {
                command.CommandType = _context.CommandType == CommandType.StoredProc ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;

                if (_context.HasParameters)
                {
                    foreach (var parameter in _context.Parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                if (_context.AddReturnValue)
                {
                    SqlParameter returnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnValue);
                }

                if (_context.Connection.State != ConnectionState.Open)
                {
                    _context.Connection.Open();
                }

                return command;
            }
        }
    }
}