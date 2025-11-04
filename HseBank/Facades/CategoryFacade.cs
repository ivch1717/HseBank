using HseBank.Factories;
using HseBank.Repository;
using HseBank.TypeOperation;

namespace HseBank.Facades;

public class CategoryFacade : ICategoryFacade
{
    ICategoryRepository  _repository;
    ICategoryFactory _factory;
    IFactoryTypeResolver _typeResolver;

    public CategoryFacade(ICategoryRepository repository,  ICategoryFactory factory,  IFactoryTypeResolver typeResolver)
    {
        _repository = repository;
        _factory = factory;
        _typeResolver = typeResolver;
    }

    public void AddCategory(string name, string typeName)
    {
        ITypeOperation type = _typeResolver.GetFactory(typeName).Create();
        int id = _repository.GetRep().Keys.Max() + 1;
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
}