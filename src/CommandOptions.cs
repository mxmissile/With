namespace RussellEast.DataAccessBuilder
{
    internal class CommandOptions : CommandExecutor, ICommandOptions
    {
        private readonly CommandBuilder commandBuilder;

        public CommandOptions(Context context, CommandBuilder commandBuilder) : base(context)
        {
            this.commandBuilder = commandBuilder;
        }
        
        public IParameterCollector And
        {
            get { return commandBuilder; }
        }
    }
}