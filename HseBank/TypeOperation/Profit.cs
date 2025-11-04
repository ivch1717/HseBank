namespace HseBank.TypeOperation;

public class Profit : ITypeOperation
{
    public string Name { get; init; }

    public Profit()
    {
        Name = "Доход";
    }
    public int Count(int balance, int amount)
    {
        return balance + amount;
    }
}