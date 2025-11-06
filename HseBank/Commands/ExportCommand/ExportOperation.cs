using HseBank.Facades;
namespace HseBank.Commands.ExportCommand;

public class ExportOperation : ICommand<string>
{
    IOperationFacade _operationFacade;

    public ExportOperation(IOperationFacade operationFacade)
    {
        _operationFacade = operationFacade;
    }

    public void Execute(string filename)
    {
        _operationFacade.ExportOperation(filename);
    }
}