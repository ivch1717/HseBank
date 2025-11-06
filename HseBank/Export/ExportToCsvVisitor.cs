using System.Text;
using HseBank.BaseClasses;
namespace HseBank.Export;

public class ExportToCsvVisitor : IExportVisitor
{
    private readonly StringBuilder _builder = new();
    private bool _headerWritten = false;
    
    public void Visit(BankAccount account)
    {
        WriteHeaderIfNeeded("Id", "Name", "Balance");
        _builder.AppendLine($"{account.Id},{account.Name},{account.Balance}");
    }

    public void Visit(Category category)
    {
        WriteHeaderIfNeeded("Id", "Name", "Type");
        _builder.AppendLine($"{category.Id},{category.Name},{category.Type.RussianName}");
    }

    public void Visit(Operation operation)
    {
        WriteHeaderIfNeeded("Id", "Type", "BankAccountId", "Amount", "Date", "CategoryId", "Description");
        _builder.AppendLine($"{operation.Id},{operation.Type.RussianName},{operation.BankAccountId},{operation.Amount}," +
                            $"{operation.Date:yyyy-MM-dd HH:mm},{operation.CategoryId},{operation.Description}");
    }
    
    public string GetResult() => _builder.ToString();
    
    private void WriteHeaderIfNeeded(params string[] headers)
    {
        if (_headerWritten) return;
        _builder.AppendLine(string.Join(",", headers));
        _headerWritten = true;
    }
}