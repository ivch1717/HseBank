namespace HseBank.TypeOperation;

public class Profit : ITypeOperation
{
    public string Name { get; init; }

    public Profit()
    {
        Name = "Profit";
    }
    public int Count(int balance, int amount)
    {
        return balance + amount;
    }
}