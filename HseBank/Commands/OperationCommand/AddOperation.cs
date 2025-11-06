using HseBank.Facades;
namespace HseBank.Commands.OperationCommand;

public class AddOperation : ICommand<OperationRequest>
{
    IOperationFacade _facade;
    public AddOperation(IOperationFacade facade) => _facade = facade;

    public void Execute(OperationRequest request)
    {
        _facade.AddOperation(request.BankAccountId, request.Amount, request.CategoryId, request.Description);
    }
}