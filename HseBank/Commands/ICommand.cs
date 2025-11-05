namespace HseBank.Commands;

public interface ICommand<in TRequest>
{
    void Execute(TRequest request);
}