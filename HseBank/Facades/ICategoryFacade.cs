namespace HseBank.Facades;

public interface ICategoryFacade
{
    public void AddCategory(string name, string type);
    public void RemoveCategory(int id);
    public void ChangeName(int id, string name);
}