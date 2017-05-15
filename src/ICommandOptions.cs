namespace RussellEast.DataAccessBuilder
{
    public interface ICommandOptions : ICommandExecutor
    {
        IParameterCollector And { get; }
    }
}