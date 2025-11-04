using HseBank.TypeOperation;

namespace HseBank.BaseClasses;

public class BankAccount : IObserver
{
    public int Id { get; init; }
    public string Name { get; set; }
    public int Balance { get; private set; }
    
    public BankAccount(int id, string name, int balance)
    {
        Id = id;
        Name = name;
        Balance = balance;
    }

    public void Update(ITypeOperation typeOperation, int amount)
    {
        if (typeOperation.Name == "Profit" && amount > Balance)
        {
            throw new ArgumentException("нельяз отменить операцию, так как на счёте нет достаточных средств");
        }
        Balance = typeOperation.Count(Balance, amount);
    }
}