using HseBank.Facades;
namespace HseBank.Commands.OperationCommand;

public class RemoveOperation : ICommand<int>
{
    IOperationFacade _facade;
    public RemoveOperation(IOperationFacade facade) => _facade = facade;

    public void Execute(int id)
    {
        _facade.RemoveOperation(id);
    }
}