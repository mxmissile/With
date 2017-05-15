namespace RussellEast.DataAccessBuilder
{
    public class With
    {
        private Context _context;
        private ConnectionOptions _connectionOptions;

        private With()
        {
            BuildObjectGraph();
        }

        public static IConnectionOptions StoredProcedure(string sprocName)
        {
            With instance = new With
            {
                _context =
                {
                    CommandType = CommandType.StoredProc, 
                    CommandText = sprocName
                }
            };

            return instance._connectionOptions;
        }

        public static IConnectionOptions SqlStatement(string sqlStatement)
        {
            With instance = new With
            {
                _context =
                {
                    CommandType = CommandType.SqlStatement, 
                    CommandText = sqlStatement
                }
            };

            return instance._connectionOptions;
        }

        private void BuildObjectGraph()
        {
            _context = new Context();

            _connectionOptions = new ConnectionOptions(_context);
        }
    }
}
