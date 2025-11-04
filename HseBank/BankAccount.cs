namespace HseBank;

public class BankAccount
{
    public int Id { get; init; }
    public string Name { get; set; }
    public int Balance { get; init; }
    
    public BankAccount(int id, string name, int balance)
    {
        Id = id;
        Name = name;
        Balance = balance;
    }
    
}