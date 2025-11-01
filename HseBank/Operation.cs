namespace HseBank;

public class Operation
{
    public int Id { get; init; }
    public string Type { get; init; }
    public int Bank_account_id { get; init; }
    public int Amount { get; init; }
    public DateTime Date { get; init; }
    public string Description { get; init; }
    public int Category_id { get; init; }

    public Operation(int id, string type, int balance, int amount, DateTime date, string description, int category_id)
    {
        Id = id;
        Type = type;
        Bank_account_id = balance;
        Amount = amount;
        Date = date;
        Description = description;
        Category_id = category_id;
    }
}