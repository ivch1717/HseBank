using System.Text;
using HseBank.BaseClasses;
namespace HseBank.Visitor;

public class ExportToCsvVisitor : IExportVisitor
{
    private readonly StringBuilder _builder = new();

    public void Visit(BankAccount account)
        => _builder.AppendLine($"{account.Id},{account.Name},{account.Balance}");

    public void Visit(Category category)
        => _builder.AppendLine($"{category.Id},{category.Name},{category.Type.Name}");

    public void Visit(Operation operation)
        => _builder.AppendLine($"{operation.Id},{operation.Type.Name},{operation.BankAccountId},{operation.Amount},{operation.Date:yyyy-MM-dd HH:mm},{operation.CategoryId},{operation.Description}");
    
    
}