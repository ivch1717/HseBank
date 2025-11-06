using System.Text.Json;
using HseBank.BaseClasses;

namespace HseBank.Export;

public class ExportToJsonVisitor : IExportVisitor
{
    private readonly List<Dictionary<string, object>> _objects = new();
    
    public void Visit(BankAccount account)
    {
        _objects.Add(new Dictionary<string, object>
        {
            { "Id", account.Id },
            { "Name", account.Name },
            { "Balance", account.Balance }
        });
    }
    
    public void Visit(Category category)
    {
        _objects.Add(new Dictionary<string, object>
        {
            { "Id", category.Id },
            { "Name", category.Name },
            { "Type", category.Type.RussianName }
        });
    }
    
    public void Visit(Operation operation)
    {
        _objects.Add(new Dictionary<string, object>
        {
            { "Id", operation.Id },
            { "Type", operation.Type.RussianName },
            { "BankAccountId", operation.BankAccountId },
            { "Amount", operation.Amount },
            { "Date", operation.Date.ToString("yyyy-MM-dd HH:mm") },
            { "CategoryId", operation.CategoryId },
            { "Description", operation.Description }
        });
    }
    
    public string GetResult()
    {
        return JsonSerializer.Serialize(_objects, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
    
}