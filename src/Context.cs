using System.Collections.Generic;
using System.Data;

namespace RussellEast.DataAccessBuilder
{
    internal class Context
    {
        private Dictionary<string, object> parameters;

        public CommandType CommandType { get; set; }
        public string CommandText { get; set; }
        public IDbConnection Connection { get; set; }
        public bool IsSuppliedConnection { get; set; }
        public bool HasParameters { get; set; }
        public bool AddReturnValue { get; set; }

        public Dictionary<string, object> Parameters
        {
            get { return parameters; }
        }

        public void AddParameter(string name, object value)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            else
            {
                if (parameters.ContainsKey(name))
                {
                    return;
                }
            }

            parameters.Add(name, value);

            HasParameters = true;
        }
    }
}