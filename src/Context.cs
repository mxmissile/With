using System.Collections.Generic;
using System.Data;

namespace RussellEast.DataAccessBuilder
{
    internal class Context
    {
        public CommandType CommandType { get; set; }
        public string CommandText { get; set; }
        public IDbConnection Connection { get; set; }
        public bool IsSuppliedConnection { get; set; }
        public bool HasParameters { get; set; }
        public bool AddReturnValue { get; set; }

        public Dictionary<string, object> Parameters { get; private set; }

        public void AddParameter(string name, object value)
        {
            if (Parameters == null)
            {
                Parameters = new Dictionary<string, object>();
            }
            else
            {
                if (Parameters.ContainsKey(name))
                {
                    return;
                }
            }

            Parameters.Add(name, value);

            HasParameters = true;
        }
    }
}