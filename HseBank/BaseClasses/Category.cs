using HseBank.TypeOperation;
using HseBank.Export;

namespace HseBank.BaseClasses;

public class Category : IExportable
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
    
    public override string ToString()
    {
        return $"[{Id}] Категория: {Name} - Тип операции: {Type.RussianName}";
    }
    
    public void Accept(IExportVisitor visitor) => visitor.Visit(this);
}