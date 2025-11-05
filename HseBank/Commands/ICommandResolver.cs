namespace HseBank.Commands;

public interface ICommandResolver
{
    ICommand<TRequest> Resolve<TRequest>(string name, bool timed = false);
    ICommandWithResult<TRequest> ResolveWithResult<TRequest>(string name, bool timed = false);
}