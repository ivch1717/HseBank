using System.Collections.Generic;
using System.IO;

namespace HseBank.Import;

public abstract class BaseImporter<T> : IImporter<T>
{
    public List<T> Import(string filePath)
    {
        return ParseContent(Reading(filePath));
    }

    protected string Reading(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Путь к файлу не может быть пустым.");
        }

        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Файл не найден.", path);
        }
        string content = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new InvalidDataException("Файл пуст.");
        }
        return content;
    }
    protected abstract List<T> ParseContent(string content);
}