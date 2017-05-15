using System;

namespace RussellEast.DataAccessBuilder
{
    internal class CommandBuilder : ICommandBuilder
    {
        private readonly Context _context;
        private readonly ICommandOptions _commandOptions;

        public CommandBuilder(Context context)
        {
            _context = context;

            _commandOptions = new CommandOptions(context, this);
        }

        public ICommandOptions WithParameter(string name, object value)
        {
            if (value == null)
            {
                value = DBNull.Value;
            }

            _context.AddParameter(name, value);

            return _commandOptions;
        }

        public ICommandOptions AddReturnValue()
        {
            _context.AddReturnValue = true;

            return _commandOptions;
        }

        public ICommandExecutor WithNoParameters()
        {
            _context.HasParameters = false;

            return _commandOptions;
        }
    }
}