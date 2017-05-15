using System;
using System.Data;
using System.Data.SqlClient;
using NLog;
using CommandType = RussellEast.DataAccessBuilder.CommandType;

namespace RussellEast.DataAccessBuilder
{
    internal class CommandExecutor : ICommandExecutor
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly Context context;

        public CommandExecutor(Context context)
        {
            this.context = context;
        }

        public void ExecuteNonQuery()
        {
            try
            {
                BuildCommand().ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (!context.IsSuppliedConnection && context.Connection.State == ConnectionState.Open)
                {
                    context.Connection.Close();
                }
            }
        }
        
        public T ExecuteScalar<T>()
        {
            try
            {
                return (T)BuildCommand().ExecuteScalar();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if (!context.IsSuppliedConnection && context.Connection.State == ConnectionState.Open)
                {
                    context.Connection.Close();
                }
                else
                {
                    logger.Warn(string.Format("Unable to close connection, State: {0}, Connection supplied: {1}", context.Connection.State, context.IsSuppliedConnection));
                }
            }
        }

        public DynamicDbDataReader ExecuteReader()
        {
            IDbCommand command = BuildCommand();

            return new DynamicDbDataReader((SqlDataReader)command.ExecuteReader(CommandBehavior.CloseConnection));
        }

        public DynamicDbDataReader ExecuteReader(CommandBehavior commandBehavior)
        {
            IDbCommand command = BuildCommand();

            return new DynamicDbDataReader((SqlDataReader)command.ExecuteReader(commandBehavior));
        }
        
        private IDbCommand BuildCommand()
        {
            using (SqlCommand command = new SqlCommand(context.CommandText, (SqlConnection) context.Connection))
            {
                command.CommandType = context.CommandType == CommandType.StoredProc ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;

                if (context.HasParameters)
                {
                    foreach (var parameter in context.Parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                if (context.AddReturnValue)
                {
                    SqlParameter returnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnValue);
                }

                if (context.Connection.State != ConnectionState.Open)
                {
                    context.Connection.Open();
                }

                return command;
            }
        }
    }
}