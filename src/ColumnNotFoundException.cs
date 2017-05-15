using System;

namespace RussellEast.DataAccessBuilder
{
    public class ColumnNotFoundException : Exception
    {
        public ColumnNotFoundException(string name)
        {
            Name = name;
        }

        public override string Message
        {
            get
            {
                return string.Format("Column '{0}' was not found", Name);
            }
        }

        public string Name { get; private set; }

    }
}