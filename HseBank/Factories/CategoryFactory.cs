using HseBank.BaseClasses;
using HseBank.TypeOperation;
namespace HseBank.Factories;

public class CategoryFactory : ICategoryFactory
{
    public Category Create(int id, string name, ITypeOperation typeOperation)
    {
        if (name == "")
        {
            throw new ArgumentException("Имя операции");
        }
        return new Category(id, name, typeOperation);
    }
}