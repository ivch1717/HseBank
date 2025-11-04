using HseBank.TypeOperation;
using HseBank.BaseClasses;
namespace HseBank.Factories;

public interface ICategoryFactory
{
    public Category Create(int id, string name, ITypeOperation typeOperation);
}