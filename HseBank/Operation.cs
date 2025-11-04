using HseBank.TypeOperation;
namespace HseBank;

public class Operation
{
    public int Id { get; init; }
    public ITypeOperation Type { get; init; }
    public int BankAccountId { get; init; }
    public int Amount { get; init; }
    public DateTime Date { get; init; }
    public int CategoryId { get; init; }
    public string Description { get; set; }

    public Operation(int id, ITypeOperation typeOperation, int bankAccountId, int amount, DateTime date, int categoryId, string description = "")
    {
        Id = id;
        Type = typeOperation;
        BankAccountId = bankAccountId;
        Amount = amount;
        Date = date;
        CategoryId = categoryId;
        Description = description;
    }
}