namespace RussellEast.DataAccessBuilder
{
    public interface ICommandBuilder : IParameterCollector
    {
        ICommandExecutor WithNoParameters();
    }
}