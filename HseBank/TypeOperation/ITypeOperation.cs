namespace HseBank.TypeOperation;

public interface ITypeOperation
{
    public string Name { get; init; }
    public int Count(int balance, int amount);
}