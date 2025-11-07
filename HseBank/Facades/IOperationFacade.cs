namespace HseBank.Facades;

public interface IOperationFacade
{
    public void AddOperation(int bankAccountId, int amount, int categoryId, string description = "",  DateTime date = default);
    public void RemoveOperation(int id);
    public void ChangeDescription(int id, string description);
    public void ExportOperation(string filename);
    public string GetAll();
}