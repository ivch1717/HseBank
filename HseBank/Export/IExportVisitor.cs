using HseBank.BaseClasses;
namespace HseBank.Export;

public interface IExportVisitor
{
    void Visit(BankAccount account);
    void Visit(Category category);
    void Visit(Operation operation);

    public string GetResult();
}