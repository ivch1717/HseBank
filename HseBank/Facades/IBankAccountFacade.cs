namespace HseBank.Facades;

public interface IBankAccountFacade
{
    public void AddAccount(string name, int balance);
    public void RemoveAccount(int id);
    public void ChangeName(int id, string name);
    public void ExportAccount(string filename);
    public string GetAll();
}