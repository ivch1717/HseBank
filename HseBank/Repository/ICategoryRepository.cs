namespace HseBank.Repository;

public interface ICategoryRepository
{
    public void Add(int id, Category account);
    public void Remove(int id);
    public bool IsEmpty();
    public Dictionary<int, Category> GetRep();
}