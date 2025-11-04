namespace HseBank.Repository;

public interface IOperationRepository
{
    public void Add(int id, Operation account);
    public void Remove(int id);
    public bool IsEmpty();
    public Dictionary<int, Operation> GetRep();
}