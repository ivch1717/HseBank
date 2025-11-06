using HseBank.Facades;
namespace HseBank.Commands.BankAccountCommand;

public class ChangeAccountName : ICommand<AccountRenameRequest>
{
    IBankAccountFacade _facade;
    public ChangeAccountName(IBankAccountFacade facade) => _facade = facade;

    public void Execute(AccountRenameRequest request)
    {
        _facade.ChangeName(request.Id, request.NewName);
    }
}