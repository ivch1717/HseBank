using HseBank.Facades;
namespace HseBank.Commands.OperationCommand;

public class GetAllOperations : ICommandWithResult<object>
{
    IOperationFacade _facade;
    public GetAllOperations(IOperationFacade facade) => _facade = facade;

    public string Execute(object _)
    {
       return _facade.GetAll();
    }
}