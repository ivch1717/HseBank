using HseBank.Facades;
namespace HseBank.Commands.ExportCommand;

public class ExportCategory : ICommand<string>
{
    ICategoryFacade _categoryFacade;
    public ExportCategory(ICategoryFacade categoryFacade) => _categoryFacade = categoryFacade;
    
    public void Execute(string filename)
    {
        _categoryFacade.ExportCategory(filename);
    }
}