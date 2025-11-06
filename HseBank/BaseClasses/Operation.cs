using HseBank.TypeOperation;
using HseBank.Export;

namespace HseBank.BaseClasses;

public class Operation : ISubject, IExportable
{
    public int Id { get; init; }
    public ITypeOperation Type { get; set; }
    public int BankAccountId { get; init; }
    public int Amount { get; init; }
    public DateTime Date { get; init; }
    public int CategoryId { get; init; }
    public string Description { get; set; }
    
    private IObserver Observer { get; init; }

    public Operation(int id, ITypeOperation typeOperation, int bankAccountId, int amount, DateTime date, int categoryId, IObserver observer, string description = "")
    {
        Id = id;
        Type = typeOperation;
        BankAccountId = bankAccountId;
        Amount = amount;
        Date = date;
        CategoryId = categoryId;
        Observer = observer;
        Description = description;
    }

    public void Notify()
    {
        Observer.Update(Type, Amount);
    }
    
    public override string ToString()
    {
        return $"[Операция #{Id}] " +
               $"Тип: {Type.RussianName}, " +
               $"Id аккаута: {BankAccountId}, " +
               $"Сумма: {Amount}, " +
               $"Дата: {Date:dd.MM.yyyy HH:mm}, " +
               $"Id категории: {CategoryId}, " +
               $"Описание: {(string.IsNullOrWhiteSpace(Description) ? "—" : Description)}";
    }
    
    public void Accept(IExportVisitor visitor) => visitor.Visit(this);
}