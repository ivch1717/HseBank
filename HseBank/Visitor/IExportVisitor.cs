using HseBank.BaseClasses;
namespace HseBank.Visitor;

public interface IExportVisitor
{
    void Visit(BankAccount account);
    void Visit(Category category);
    void Visit(Operation operation);
}