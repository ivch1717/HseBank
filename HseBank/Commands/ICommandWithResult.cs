namespace HseBank.Commands;

public interface ICommandWithResult<in TRequest>
{
    string Execute(TRequest request);
}