namespace HseBank.Import;

public interface IImportResolver
{
    public IImporter<T> GetImporter<T>(string name);
}