using System;

namespace RussellEast.DataAccessBuilder
{
    internal class CommandBuilder : ICommandBuilder
    {
        private readonly Context context;
        private ICommandOptions commandOptions;

        public CommandBuilder(Context context)
        {
            this.context = context;

            commandOptions = new CommandOptions(context, this);
        }

        public ICommandOptions WithParameter(string name, object value)
        {
            if (value == null)
            {
                value = DBNull.Value;
            }

            context.AddParameter(name, value);

            return commandOptions;
        }

        public ICommandOptions AddReturnValue()
        {
            context.AddReturnValue = true;

            return commandOptions;
        }

        public ICommandExecutor WithNoParameters()
        {
            context.HasParameters = false;

            return commandOptions;
        }
    }
}