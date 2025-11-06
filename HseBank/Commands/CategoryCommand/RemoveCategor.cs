using HseBank.Facades;
namespace HseBank.Commands.CategoryCommand;

public class RemoveCategory : ICommand<int>
{
    ICategoryFacade _facade;
    public RemoveCategory(ICategoryFacade facade) => _facade = facade;

    public void Execute(int id)
    {
        _facade.RemoveCategory(id);
    }
}