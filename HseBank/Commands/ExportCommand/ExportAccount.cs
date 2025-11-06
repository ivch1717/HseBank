using HseBank.Facades;

namespace HseBank.Commands.ExportCommand;

public class ExportAccount : ICommand<string>
{
    IBankAccountFacade  _accountFacade;
    public ExportAccount(IBankAccountFacade accountFacade) => _accountFacade = accountFacade;
    
    public void Execute(string filename)
    {
        _accountFacade.ExportAccount(filename);
    }
}