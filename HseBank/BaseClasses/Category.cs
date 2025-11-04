using HseBank.TypeOperation;

namespace HseBank.BaseClasses;

public class Category
{
    public int Id { get; init; }
    public string Name { get; set; }
    public ITypeOperation Type { get; init; }
    
    public Category(int id, string name, ITypeOperation typeOperation)
    {
        Id = id;
        Name = name;
        Type = typeOperation;
    }
}