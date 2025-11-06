namespace HseBank.Import;

public interface IImporter<T>
{
    List<T> Import(string filePath);
}