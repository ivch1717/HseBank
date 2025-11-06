using HseBank.Facades;
namespace HseBank.Commands.BankAccountCommand;

public class GetAllAccounts : ICommandWithResult<object>
{
    IBankAccountFacade _facade;
    public GetAllAccounts(IBankAccountFacade facade) => _facade = facade;

    public string Execute(object _)
    {
        return _facade.GetAll();
    }
}