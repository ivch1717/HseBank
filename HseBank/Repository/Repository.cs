namespace HseBank.Repository;

public abstract class Repository<T>
{
    private Dictionary<int, T> Rep = new Dictionary<int, T>();
    public void Add(int id, T t)
    {
        Rep.Add(id, t);
    }

    public void Remove(int id)
    {
        Rep.Remove(id);
    }

    public Dictionary<int, T> GetRep()
    {
        return Rep;
    }

    public bool IsEmpty()
    {
        return Rep.Count == 0;
    }
}