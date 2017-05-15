namespace RussellEast.DataAccessBuilder
{
    public interface IParameterCollector
    {
        ICommandOptions WithParameter(string name, object value);
        ICommandOptions AddReturnValue();
    }
}