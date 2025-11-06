using HseBank.Facades;
namespace HseBank.Commands.CategoryCommand;

public class GetAllCategories : ICommandWithResult<object>
{
    ICategoryFacade _facade;
    public GetAllCategories(ICategoryFacade facade) => _facade = facade;

    public string Execute(object _)
    {
        return _facade.GetAll();
    }
}