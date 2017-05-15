using System.Data;

namespace RussellEast.DataAccessBuilder
{
    public interface ICommandExecutor
    {
        void ExecuteNonQuery();

        T ExecuteScalar<T>();

        DynamicDbDataReader ExecuteReader();
        DynamicDbDataReader ExecuteReader(CommandBehavior commandBehavior);
    }
}