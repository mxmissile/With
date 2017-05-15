namespace RussellEast.DataAccessBuilder
{
    internal class CommandOptions : CommandExecutor, ICommandOptions
    {
        private readonly CommandBuilder _commandBuilder;

        public CommandOptions(Context context, CommandBuilder commandBuilder) : base(context)
        {
            _commandBuilder = commandBuilder;
        }
        
        public IParameterCollector And => _commandBuilder;
    }
}