namespace HseBank.TypeOperation;

public class Profit : ITypeOperation
{
    public string Name { get; init; }
    public string RussianName { get; init; }

    public Profit()
    {
        Name = "Profit";
        RussianName = "Доход";
    }
    public int Count(int balance, int amount)
    {
        return balance + amount;
    }
}