namespace HseBank.Visitor;

public interface IExportable
{
    void Accept(IExportVisitor visitor);
}