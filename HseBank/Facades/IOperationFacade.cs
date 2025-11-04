namespace HseBank.Facades;

public interface IOperationFacade
{
    public void AddOperation(int bankAccountId, int amount, int categoryId, string description = "");
    public void RemoveOperation(int id);
    public void ChangeDescription(int id, string description);
}