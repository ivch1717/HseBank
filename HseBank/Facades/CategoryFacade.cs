using HseBank.Export;
using HseBank.Factories;
using HseBank.Repository;
using HseBank.TypeOperation;

namespace HseBank.Facades;

public class CategoryFacade : ICategoryFacade
{
    ICategoryRepository  _repository;
    ICategoryFactory _factory;
    IFactoryTypeResolver _typeResolver;
    IExportResolver _exportResolver;

    public CategoryFacade(ICategoryRepository repository,  ICategoryFactory factory,  IFactoryTypeResolver typeResolver,  IExportResolver exportResolver)
    {
        _repository = repository;
        _factory = factory;
        _typeResolver = typeResolver;
        _exportResolver = exportResolver;
    }

    public void AddCategory(string name, string typeName)
    {
        ITypeOperation type = _typeResolver.GetFactory(typeName).Create();
        int id = _repository.IsEmpty() ? 0 : _repository.GetRep().Keys.Max() + 1;
        _repository.Add(id, _factory.Create(id, name, type));
    }


    public void RemoveCategory(int id)
    {
        if (!_repository.GetRep().Keys.Contains(id))
        {
            throw new ArgumentException("id не корректный");
        }
        _repository.Remove(id);
    }
    
    public void ChangeName(int id, string name)
    {
        if (!_repository.GetRep().Keys.Contains(id))
        {
            throw new ArgumentException("id не корректный");
        }
        
        if (name == "")
        {
            throw new ArgumentException("Имя категории не может быть пустым");
        }
        _repository.GetRep()[id].Name = name;
    }

    public void ExportCategory(string filename)
    {
        if (!filename.Contains("."))
        {
            throw new ArgumentException("не праивльное название файла");
        }

        string expansion = filename.Substring(filename.LastIndexOf('.') + 1);
        IExportVisitor visitor = _exportResolver.GetVisitor(expansion);
        foreach (var repv in _repository.GetRep().Values)
        {
            repv.Accept(visitor);
        }

        string res = visitor.GetResult();
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory)
                                 .Parent?.Parent?.Parent?.FullName 
                             ?? AppContext.BaseDirectory;
        
        string dataFolder = Path.Combine(projectRoot, "Data");
        string filePath = Path.Combine(dataFolder, filename);
        File.WriteAllText(filePath, res);
    }
    
    public string GetAll()
    {
        if (_repository.IsEmpty())
        {
            return "Категорий  пока нет";
        }
        string result = "";
        foreach (var acc in _repository.GetRep().Values)
        {
            result += acc.ToString() + '\n';
        }
        return result;
    }
}