namespace HseBank.Export;

public interface IExportable
{
    void Accept(IExportVisitor visitor);
}