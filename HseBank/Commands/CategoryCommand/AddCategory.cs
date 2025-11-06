using HseBank.Facades;
namespace HseBank.Commands.CategoryCommand;

public class AddCategory : ICommand<CategoryRequest>
{
    ICategoryFacade _facade;
    public AddCategory(ICategoryFacade facade) => _facade = facade;

    public void Execute(CategoryRequest request)
    {
        _facade.AddCategory(request.Name, request.TypeName);
    }
}