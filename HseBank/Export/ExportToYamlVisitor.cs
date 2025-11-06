using HseBank.BaseClasses;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HseBank.Export;

public class ExportToYamlVisitor : IExportVisitor
{
    private readonly List<object> _objects = new();

    public void Visit(BankAccount account) => _objects.Add(new { account.Id, account.Name, account.Balance });
    public void Visit(Category category) => _objects.Add(new { category.Id, category.Name, Type = category.Type.RussianName });
    public void Visit(Operation operation) => _objects.Add(new
    {
        operation.Id,
        Type = operation.Type.RussianName,
        operation.BankAccountId,
        operation.Amount,
        Date = operation.Date.ToString("yyyy-MM-dd HH:mm"),
        operation.CategoryId,
        operation.Description
    });

    public string GetResult()
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        return serializer.Serialize(_objects);
    }
}