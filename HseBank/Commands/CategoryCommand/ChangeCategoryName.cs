using HseBank.Facades;
namespace HseBank.Commands.CategoryCommand;

public class ChangeCategoryName : ICommand<CategoryRenameRequest>
{
    ICategoryFacade _facade;
    public ChangeCategoryName(ICategoryFacade facade) => _facade = facade;

    public void Execute(CategoryRenameRequest request)
    {
        _facade.ChangeName(request.Id, request.NewName);
    }
}