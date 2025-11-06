using HseBank.Facades;
namespace HseBank.Commands.BankAccountCommand;

public class RemoveAccount : ICommand<int>
{
    IBankAccountFacade _facade;
    public RemoveAccount(IBankAccountFacade facade) => _facade = facade;

    public void Execute(int id)
    {
        _facade.RemoveAccount(id);
    }
}