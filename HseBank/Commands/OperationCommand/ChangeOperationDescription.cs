using HseBank.Facades;
namespace HseBank.Commands.OperationCommand;

public class ChangeOperationDescription : ICommand<OperationChangeDescriptionRequest>
{
    IOperationFacade _facade;
    public ChangeOperationDescription(IOperationFacade facade) => _facade = facade;

    public void Execute(OperationChangeDescriptionRequest request)
    {
        _facade.ChangeDescription(request.Id, request.Description);
    }
}