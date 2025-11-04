using HseBank.TypeOperation;
namespace HseBank.Factories;

public interface ICategoryFactory
{
    public Category Create(int id, string name, ITypeOperation typeOperation);
}