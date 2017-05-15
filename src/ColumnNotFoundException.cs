using System;

namespace RussellEast.DataAccessBuilder
{
    public class ColumnNotFoundException : Exception
    {
        public ColumnNotFoundException(string name)
        {
            Name = name;
        }

        public override string Message => $"Column '{Name}' was not found";

        public string Name { get; }
    }
}