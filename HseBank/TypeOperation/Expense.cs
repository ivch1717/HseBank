namespace HseBank.TypeOperation;

public class Expense : ITypeOperation
{
    public string Name { get; init; }

    public Expense()
    {
        Name = "Расход";
    }
    public int Count(int balance, int amount)
    {
        if (amount > balance)
        {
            throw new ArgumentException("На счёте нет столько денег.");
        }
        return balance - amount;
    }
}