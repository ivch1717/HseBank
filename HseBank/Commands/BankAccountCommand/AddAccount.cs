using HseBank.Facades;
namespace HseBank.Commands.BankAccountCommand;

public class AddAccount : ICommand<AccountRequest>
{
    IBankAccountFacade _facade;
    public AddAccount(IBankAccountFacade facade) => _facade = facade;

    public void Execute(AccountRequest request)
    {
        _facade.AddAccount(request.Name, request.Balance);
    }
}