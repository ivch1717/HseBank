namespace HseBank.Export;

public interface IExportResolver
{
    public IExportVisitor GetVisitor(string name);
}